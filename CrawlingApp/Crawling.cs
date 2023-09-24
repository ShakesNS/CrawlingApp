using HtmlAgilityPack;
using RandomUserAgent;
using System.Text;
using System.Web;
namespace CrawlingApp
{
    public class Crawling
    {
        // Linkleri tutacağım string nesnesi alan liste tipinde sınıf özelliği
        public List<string>? Links { get; set; }

        // İnternet sitesinin kaynak kodunu alabilceğim HtmlDocument tipinde sınıf özelliği
        public HtmlDocument? Page { get; set; }

        // Erişmek istediğim internet sitesinin Host'unu tutan string tipinde sınıf özelliği
        public string Host { get; set; }

        // Linklerden alacağım verileri istediğim türde tutabilmek için kullandığım dictionary tipinde sınıf özelliği
        public Dictionary<string, string>? Advertisements { get; set; }

        // Boş constructor
        public Crawling(){}

        // Parametre alan constructor, host bilgisini burdan alıyorum ve üretilmesi gereken nesneleri üretiyorum 
        public Crawling(string host)
        {
            Host = host;
            Links = new List<string>();
            Advertisements = new Dictionary<string, string>();
        }

        // Erişmek istediğim internet sitesinin kaynak kodunu alabilmek için tanımladığım fonksiyon
        public void GetPage()
        {
            //Hata ayıklama bloğu
            try
            {
                var url = this.Host;
                // İnternet adresinin doğru biçimde kullanılması için ufak bir kontrol
                if (!url.StartsWith("https://") && !url.StartsWith("http://"))
                    url = "https://" + url;
                // İnternet sitesinin adresi ile sayfayı bir HtmlWeb nesnesinin içine atıyorum.
                var web = new HtmlWeb();
                // Olası IP banlanmasına karşı olarak bir random UserAgent Header oluşturan kütüphane kullanmak istedim.
                web.UserAgent = RandomUa.RandomUserAgent;
                var document = web.Load(url);
                this.Page = document;
            }
            catch (Exception)
            {
                // Herhangi bir hata durumunda hatayı göstermesi için gereken kod parçası
                throw;
            }
        }

        // Erişmek istediğim sitenin linklerine ulaşmak için HtmlDocument parametreli fonksiyon
        public void GetLinks(HtmlDocument? page)
        {
            //Hata ayıklama bloğu
            try
            {
                // HtmlDocument nesnesini Parse ederek kaynak kodlarına ulaşıyorum.
                var body = page.ParsedText;
                // Sitenin kaynak kodlarına bakarak almak istediğim kısmı <div> etiketi ile ayırıyorum.
                var div = page.DocumentNode.SelectSingleNode($"//div[@class='showcase row']");
                // Nesne boş değilse yani istediğim gibi çalıştı ise linklerin bulunduğu <a href </a> etiketlerini ayırıyorum
                if (div!=null)
                {
                    this.Links = div.Descendants("a")
                    .Select(n => n.GetAttributeValue("href", string.Empty))
                    .Where(u => !String.IsNullOrEmpty(u))
                    .ToList();               
                }
                /* 
                  
                    Tanımladığım özelliğe elde ettiğim link listesini atıyorum 
                    ve Distinct() fonksiyonunu kullanarak aynı olan linkleri ayırıyorum
                    çünkü aynı linklere tekrar tekrar istek atmak hem görünüm hem iş gücü
                    olarak kötü bir durum               
                */
                this.Links = this.Links.Distinct().ToList();              
            }
            catch (Exception)
            {
                // Herhangi bir hata durumunda hatayı göstermesi için gereken kod parçası
                throw;
            }
        }

        /* 
            Elde ettiğim linklerin her birine istek atarak istediğim verileri çekmek için kullandığım fonksiyon
            Paremetre olarak istek atacağım linkin host ve yolunu alıyorum                   
        */
        public void GetInfo(string host,IEnumerable<string> links)
        {
            //Hata ayıklama bloğu
            try
            {
                // Elimdeki site yollarına tek tek istek atabilmek için liste içinde kullandığım döngü
                foreach (var url in links)
                {
                    // Liste içinde hatalı bir yol olmasına karşılık kontrol kod parçası
                    if (!url.StartsWith("/ilan"))
                        continue;
                    // Paremetrelerden gelen değerleri birleştirerek düzgün bir Url elde etmek için kullandığım kod parçası
                    StringBuilder sb = new StringBuilder();
                    sb.Append(host).Append(url);
                    /* 
                        Oluşan Url üzerinden yukarıda yaptığım gibi kaynak kodu üzerinden uygun olan
                        etiketleri ayırarak istediğim verileri alıyorum                    
                    */
                    var web = new HtmlWeb();
                    var document = web.Load(sb.ToString());
                    var adName = document.DocumentNode.SelectSingleNode($"//div[@class='product-name-container']").InnerText.Trim();
                    var adPriceEl = document.DocumentNode.SelectSingleNode($"//div[@class='product-price']");
                    var adDiscount = adPriceEl.SelectSingleNode("//div[@class='price-discount']");
                    // Aldığım verilerde herhangi bir hata olmaması için alt etiketi var ise siliyorum
                    if (adDiscount != null)
                    {                       
                        adDiscount.Remove();
                    }
                    var adPrice = adPriceEl.InnerText.Trim();
                    /* 
                        Verileri daha düzenli ve doğru kullanabilmek için HtmlDecode yapıyorum,
                        sağ ve solundaki boşlukları temizliyorum ve elde ettiğim verilerde alt
                        satıra geçilen bir boşluk var ise onları kaldırıyorum                  
                    */
                    adName = HttpUtility.HtmlDecode(adName).Trim().Replace("\n","");
                    adPrice = HttpUtility.HtmlDecode(adPrice).Trim().Replace("\n", "");

                    // Elde ettiğim verileri tanımladığım özelliğe ekliyorum.
                    Advertisements.Add(adName, adPrice);                  
                }            
            }
            catch (Exception)
            {
                // Herhangi bir hata durumunda hatayı göstermesi için gereken kod parçası
                throw;
            }
        }
    }
}
