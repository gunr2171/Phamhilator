﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



namespace Phamhilator
{
    public static class TermReader
    {
        public static List<Term> ReadTerms(string filePath, FilterType filterType)
        {
            List<JsonTerm> data;
            var terms = new List<Term>();

            try
            {
                data = JsonConvert.DeserializeObject<List<JsonTerm>>(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Can't read file '{0}'. Reason: {1}", filePath, ex.Message), ex);
            }

            if (data == null) { return terms; }

            foreach (var t in data)
            {
                terms.Add(t.ToTerm(filterType));
            }

            return terms;
        }
    }
}
