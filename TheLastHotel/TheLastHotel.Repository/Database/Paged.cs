using System;
using System.Collections.Generic;
using System.Text;

namespace TheLastHotel.Repository.Database
{
    public class Paged<T> where T : class
    {

        public int Take { get; set; }
        public int Skip { get; set; }
        public long TotalItemCount { get; set; }
        public List<T> Content { get; set; }
        public string OrderBy { get; set; }

        public int PageNumber
        {
            get { if (Skip == 0) return 1; return (Skip / Take) + 1; }
        }

        public int PageSize
        {
            get { return Take; }
        }

        public int PageCount
        {
            get
            {
                var totalItemCount = Convert.ToInt32(TotalItemCount);

                if (totalItemCount == Take || totalItemCount == 0)
                    return 1;

                else if ((TotalItemCount / Take) > 0 && (TotalItemCount % Take) == 0)
                    return (totalItemCount / Take);

                else if ((TotalItemCount / Take) > 0 && (TotalItemCount % Take) > 0)
                    return (totalItemCount / Take) + 1;

                else
                    return (totalItemCount / Take) + 1;
            }
        }
    }
}


