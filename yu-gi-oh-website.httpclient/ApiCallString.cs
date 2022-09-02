﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh_website.httpclient
{

    internal class ApiCallString
    {
          
      

        internal string GetAllTCGCardsString(DateTime inputStartDate,DateTime? inputEndDate = null)
        {
            if (!inputEndDate.HasValue)
            {
                inputEndDate = DateTime.Now;
            }

            var startDate = inputStartDate.Date.ToString("MM/dd/yyyy");
            var endDate = inputEndDate.Value.Date.ToString("MM/dd/yyyy");

            return $"https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate={startDate}&enddate={endDate}&dateregion=tcg_date&misc=yes";
           
        } 
    }
}