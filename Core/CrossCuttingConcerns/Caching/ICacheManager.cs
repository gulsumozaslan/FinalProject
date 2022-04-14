using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);    //generic olmayan veriyonu ama tip dönüşümü yapmak gerekir
        void Add(string key, object value, int duration);
        bool IsAdd(string key);   //cache'de var mı?
        void Remove(string key);  //key vericem cache'den uçuracak
        void RemoveByPattern(string pattern);  // İçinde Get olanlar veya içinde kategori olanları temizle gibi (regex kullanarak)
    }
}
