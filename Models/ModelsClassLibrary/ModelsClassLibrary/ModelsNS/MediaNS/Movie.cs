using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.MediaNS;

namespace ModelsClassLibrary.Models.MediaNS
{
    public class Movie:CommonWithId
    {
        public string Location { get; set; }
        public int Version { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        public string MediaViewFile
        {
            get
            {
                try
                {
                    var base64 = Convert.ToBase64String(Content);
                    var imgSrc = String.Format("data:{0};base64,{1}", ContentType, base64);

                    return imgSrc;
                }
                catch
                {
                    throw;
                }
            }
        }

    }
}