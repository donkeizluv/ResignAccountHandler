using System;
using ResignAccountHandlerUI.ResignExtractor;

namespace ResignAccountHandlerUI.Model
{
    public class Resignation
    {
        public int Id { get; set; }
        public string ADName { get; set; } //set by ResignInfoExtractor
        public DateTime ResignDay { get; set; } //set by ResignInfoExtractor
        public string HRCode { get; set; } //set by ResignInfoExtractor
        public DateTime ReceiveDate { get; set; } //set by ResignInfoExtractor
        public RecordStatus Status { get; set; } //undetermined

        public string Manager { get; set; } //for autoreply purposes

        public string ErrorMessage { get; private set; } //AppendErrorMessage method

        //public Resignation()
        //{
        //    Manager = string.Empty;
        //    ManagerEmail = string.Empty;
        //}



        public void SetErrorMessage(string mess) //update purpose
        {
            ErrorMessage = mess;
        }

        public void AppendErrorMessage(string mess) //normal purpose
        {
            ErrorMessage += " " + mess;
        }
        //centralize format
        //adding new field guide
        //add new column in Query viewer
        //add new added element to ToArray respected to its column index in QueryViewer
        //update editor acordingly
        public string[] ToArray(bool skipErorrMessage = true)
        {
            return new string[] {Id.ToString(), ADName, ResignDay.ToString("dd/MM/yyyy hh:mm:ss tt"), HRCode,
                ReceiveDate.ToString("dd/MM/yyyy hh:mm:ss tt"), Status.ToString(), Manager, skipErorrMessage? null : ErrorMessage};
        }
    }
}
