using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MMDisco
{
    class DiscogsSearchResult
    {
        public String title {get; set;}
        public String year {get; set;}
        public String id {get; set;}

        public override string ToString()
        {
            if (year != null)
                return title + " (" + year + ")";
            else
                return title;
        }
    }

    class DiscogsTrack
    {
        public String duration { get; set; }
        public String position { get; set; }
        public String title { get; set; }

        public override string ToString()
        {
            return position + " - " + title;
        }
    }

    class DiscogsHelper
    {
        public static IList<DiscogsSearchResult> search(String searchString, String type = "release")
        {
            HttpWebRequest h = (HttpWebRequest)WebRequest.Create("http://api.discogs.com/database/search?q=" + searchString + "&type=" + type);
            h.UserAgent = "discogs for mediamonkey v2";
            h.Method = "GET";
            HttpWebResponse r = (HttpWebResponse)h.GetResponse();
            Stream rec = r.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(rec, encode);
            JObject search = JObject.Parse(readStream.ReadToEnd());
            IList<JToken> results = search["results"].Children().ToList();
            IList<DiscogsSearchResult> l = new List<DiscogsSearchResult>();
            foreach(JToken res in results)
            {
                DiscogsSearchResult d = JsonConvert.DeserializeObject<DiscogsSearchResult>(res.ToString());
                l.Add(d);
            }
            return l;
        }

        public static IList<DiscogsTrack> loadTracks(DiscogsSearchResult d)
        {
            HttpWebRequest h = (HttpWebRequest)WebRequest.Create("http://api.discogs.com/releases/" + d.id);
            h.UserAgent = "discogs for mediamonkey v2";
            h.Method = "GET";
            HttpWebResponse r = (HttpWebResponse)h.GetResponse();
            Stream rec = r.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(rec, encode);
            JObject search = JObject.Parse(readStream.ReadToEnd());
            IList<JToken> results = search["tracklist"].Children().ToList();
            IList<DiscogsTrack> l = new List<DiscogsTrack>();
            foreach (JToken res in results)
            {
                DiscogsTrack t = JsonConvert.DeserializeObject<DiscogsTrack>(res.ToString());
                l.Add(t);
            }
            return l;
        }
    }
}
