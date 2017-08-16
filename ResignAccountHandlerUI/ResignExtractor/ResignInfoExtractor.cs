using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ResignAccountHandlerUI.Model;

namespace ResignAccountHandlerUI
{
	public enum RecordStatus
	{
		Erorr,
		Ready,
		Disabled,
		Canceled, //cancel resignation
		Deleted, //deleted, stay for record
		OnlyDisable //scheduled disable, for maternity leave and stuff
	}

	public enum ParseResult
	{
		Parsed_Info_Error,
		Not_Resign_Email,
		OK,
	}
	[Flags]
	public enum FormType
	{
		NotSet = 0,
		Unknown = 1,
		Resign = 2,
		CancelCode = 4,
		CancelResign = 8
	}
	public static class Extention
	{
		public static bool HasMoreThanOneFlag(this FormType t)
		{
			return (t & (t - 1)) != 0;
		}
	}


	public class ResignInfoExtractor
	{
		private const string MailSeperatorOutlook =
			"<div style=\'border:none;border-top:solid #B5C4DF 1.0pt;padding:3.0pt 0in 0in 0in\'>";
		private const string MailSeperatorImap =
			"<div style=\"border:none;border-top:solid #B5C4DF 1.0pt;padding:3.0pt 0in 0in 0in\">";
		private List<string> Seperators = new List<string>();
		//private const string InfoXpathBold = @"//td[2]/p/b[1]/span";
		//private const string InfoXpath = @".//p";

		private const string TablesXpath = "//table[@class='MsoNormalTable']";

		#region Regz text
		//resign regz
		private const string RegcognizeTextResignForm_1 = @"Thời hạn cuối cùng HR nhận hồ sơ nghỉ việc (*)";
		private const string RegcognizeTextResignForm_2 = @"(chờ điều tra, xác minh…)";
		//cancel code regz
		private const string RegcognizeTextCancelCode = @"Hủy code"; //1,1
		//cancel resign regz
		private const string RegcognizeTextCancelResign = @"Hủy thông tin nghỉ việc";
		#endregion




		private const int InfoColumnIndex = 1;

		//"//span[@class='btn btn-success download-button']";
		public ResignInfoExtractor()
		{
			AddSeperators();
		}
		private void AddSeperators()
		{
			Seperators.Add(MailSeperatorImap);
			Seperators.Add(MailSeperatorOutlook);
		}
		/// <summary>
		/// false means content is not resign letter, null means cant parse for info -> see rawInfo
		/// </summary>
		/// <param name="html"></param>
		/// <param name="receiveDate"></param>
		/// <param name="resign"></param>
		/// <param name="rawInfo"></param>
		/// <returns></returns>
		public ParseResult ExtractResignForm(string html, DateTime receiveDate, out Resignation resign, out string errorMess)
		{
			
			try
			{
				var t = DetermindFormType(html, out var tableNode);
				if (ParseResignInfo(tableNode, t, out resign, out errorMess))
				{
					resign.ReceiveDate = receiveDate;
					return ParseResult.OK;
				}
				else
				{
					errorMess = "Not a resign email.";
					return ParseResult.Not_Resign_Email;
				}
			}
			catch (ArgumentException ex)
			{
				resign = null;
				errorMess = ex.Message;
				return ParseResult.Parsed_Info_Error;
			}
			catch (InvalidOperationException ex)
			{
				resign = null;
				errorMess = ex.Message;
				return ParseResult.Parsed_Info_Error;
			}
			
		}

		//run.SelectNodes("//div[@class='date']")
		//Will will behave exactly like doc.DocumentNode.SelectNodes("//div[@class='date']")
		//run.SelectNodes("./div[@class='date']")
		//Will give you all the<div> nodes that are children of run node.It won't search deeper, only at the very next depth level.
		//run.SelectNodes(".//div[@class='date']")
		//Will return all the<div> nodes with that class attribute, but not only next to the run node, but also will search in depth(every possible descendant of it)

		private bool ParseResignInfo(HtmlNode table, FormType t, out Resignation resign, out string errorMess)
		{
			errorMess = string.Empty;
			resign = null;
			switch (t)
			{
				case FormType.NotSet:
					throw new InvalidProgramException("form type is not set");
				case FormType.Unknown:
					return false;
				case FormType.Resign:
					ParseResign(table, out resign);
					break;
				case FormType.CancelCode:
					ParseCancelCode(table, out resign);
					break;
				case FormType.CancelResign:
					ParseCancelResign(table, out resign);
					break;
				default:
					throw new InvalidProgramException("invalid form type");
			}
			errorMess = t.ToString();
			return true;

		}

		//0,1: realname
		//1,1: hrcode
		//2,1: indus code
		//3,1: email
		//4,1: resign date
		private bool ParseResign(HtmlNode tableNode, out Resignation resign)
		{
			resign = new Resignation();
			if (GetTextFromCell(tableNode, 1, 1, out var hrCode))
				resign.HRCode = hrCode;
			else
				throw new ArgumentException("Cant get hrCode");
			//get email
			if (GetTextFromCell(tableNode, 3, 1, out var ad))
				resign.ADName = ad;
			else
				throw new ArgumentException("Cant get Ad");
			//get resign date
			if (GetTextFromCell(tableNode, 4, 1, out var resignDateText))
			{
				if (DateTime.TryParseExact(FixDateText(resignDateText),
				@"dd/MM/yyyy",
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var resignDate))
					resign.ResignDay = resignDate;
				else
					throw new ArgumentException("Cant parse resign text to Date.");
			}
			else
				throw new ArgumentException("Cant get resign date text.");
			resign.Status = RecordStatus.Ready;
			return true;

		}
		private static string FixDateText(string date)
		{
			string AddZero(string s)
			{
				if(s.Length < 2)
				{
					return string.Format("{0}{1}", "0", s);
				}
				return s;
			}
			var split = date.Replace(@"//", @"/").Split('/');
			if (split.Count() != 3) return date; //invalid
			split[0] = AddZero(split[0]);
			split[1] = AddZero(split[1]);
			return string.Join("/", split);
		}
		//2,1: Hr code
		//3,1: email
		private bool ParseCancelResign(HtmlNode tableNode, out Resignation resign)
		{
			resign = new Resignation();
			//get hr code
			if (GetTextFromCell(tableNode, 2, 1, out var hrCode))
				resign.HRCode = hrCode;
			else
				throw new ArgumentException("Cant get hrCode");

			//get ad
			if (GetTextFromCell(tableNode, 3, 1, out var ad))
				resign.ADName = ad;
			else
				throw new ArgumentException("Cant get Ad");

			resign.ResignDay = System.Windows.Forms.DateTimePicker.MaximumDateTime;
			resign.Status = RecordStatus.Canceled;
			return true;

		}
		//pretty much the same with resign
		//2,1: Hr code
		//3,1: email
		private bool ParseCancelCode(HtmlNode tableNode, out Resignation resign)
		{
			resign = new Resignation();
			//get hr code
			if (GetTextFromCell(tableNode, 2, 1, out var hrCode))
				resign.HRCode = hrCode;
			else
				throw new ArgumentException("Cant get hrCode");

			//get ad
			if (GetTextFromCell(tableNode, 3, 1, out var ad))
				resign.ADName = ad;
			else
				throw new ArgumentException("Cant get Ad");

			resign.Status = RecordStatus.Ready;
			resign.ResignDay = DateTime.Today;
			return true;
		}

		//clean string here
		private bool GetTextFromCell(HtmlNode tableNode, int rowIndex, int colIndex, out string innerText)
		{
			innerText = string.Empty;
			try
			{
				var tr = tableNode.SelectNodes(".//tr")[rowIndex];
				innerText = tr.SelectNodes(".//td")[colIndex].InnerText.Replace("&nbsp;", "").Trim();

				return true;
			}
			catch (IndexOutOfRangeException)
			{
				return false;
			}
			catch (NullReferenceException)
			{
				return false;
			}
			catch(ArgumentOutOfRangeException)
			{
				return false;
			}
		}

		//kind of unessesary, but lol i like flags
		private FormType DetermindFormType(string html, out HtmlNode tableNode)
		{
			HtmlNode table;
			tableNode = null;

			var t = FormType.NotSet;
			var lastestEmailContent = GetLastestEmail(html);
			var doc = new HtmlDocument();
			doc.LoadHtml(lastestEmailContent);
			if (FindRegconizeText(doc, out table, new string[] { RegcognizeTextResignForm_1, RegcognizeTextResignForm_2 }))
			{
				t |= FormType.Resign;
				tableNode = table;

			}
			if(FindRegconizeText(doc, out table, new string[] { RegcognizeTextCancelCode }))
			{
				t |= FormType.CancelCode;
				tableNode = table;
			}
			if(FindRegconizeText(doc, out table, new string[] { RegcognizeTextCancelResign }))
			{
				t |= FormType.CancelResign;
				tableNode = table;
			}
			//not ok to have 2 valid flags
			if (t.HasMoreThanOneFlag())
				throw new InvalidOperationException("Form matched multiple types.");
			//check if any flags been set
			if ((t & (FormType.CancelCode | FormType.CancelResign | FormType.Resign | FormType.Unknown)) == 0)
			{
				t = FormType.Unknown;
			}
			else //valid -> check table rows
			{
				if(!GetTableRowSpec(t).Contains(TableRowCount(tableNode)))
					throw new InvalidOperationException("Form row count is not correct.");
			}

			return t;
		}   
		private static int[] GetTableRowSpec(FormType t)
		{
			switch (t)
			{
				case FormType.NotSet:
					throw new InvalidProgramException();
				case FormType.Unknown:
					throw new InvalidProgramException(); ;
				case FormType.Resign:
                    return new int[] { 11, 13 };

				case FormType.CancelCode:
                    return new int[] { 4 };
                case FormType.CancelResign:
                    return new int[] { 5 };
                default:
					throw new InvalidProgramException();
			}
		}
		private string CleanString(string s)
		{
			return s.Replace("\r\n", string.Empty).
				Replace("\r", string.Empty).
				Replace("\n", string.Empty).
				Replace(" ", string.Empty);
		}

		private string GetLastestEmail(string mailHtml)
		{
			string[] array = new string[0];
			string[] temp;
			foreach (var sep in Seperators)
			{
				temp = SplitByString(mailHtml, sep);
				if(temp.Count() > array.Count())
				{
					array = temp;
				}
			}
			return array.First();
		}
		private int TableRowCount(HtmlNode table)
		{
			if (table == null) return 0;
			var tr = table.SelectNodes(".//tr");
			if (tr == null) return 0;
			return tr.Count;
		}
		private bool FindRegconizeText(HtmlDocument doc, out HtmlNode tableNode, params string[] regz)
		{
			tableNode = null;
			var tables = doc.DocumentNode.SelectNodes(TablesXpath);
			if (tables == null) return false;
			int contains = 0;
			var skipList = new List<string>();
			foreach (var table in tables)
			{
				foreach (var reg in regz.Except(skipList))
				{
					if (CleanString(table.InnerText).Contains(CleanString(reg)))
					{
						skipList.Add(reg);
						contains++;

					}
				}
				if (contains == regz.Count())
				{
					tableNode = table;
					return true;
				}
			}
			return false;
		}
		private static string[] SplitByString(string tobeSplitted, string splitter)
		{
			return tobeSplitted.Split(new[] { splitter }, StringSplitOptions.None);
		}
	}
}