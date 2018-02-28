using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovingPicture.DAL
{
    public class DataSettings
    {
        public string dataFilePah = HttpContext.Current.Server.MapPath("~/App_Data/MovieInformation.xml");
    }
}