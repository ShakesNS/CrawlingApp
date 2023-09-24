# CrawlingApp
- Crawling Application belirli bir E-ticaret sitesinin ana sayfasında bulunan "Ana Sayfa Vitrini" bölümündeki ürünlerin isimleri ve ürün fiyatlarını okuyarak liste şeklinde konsol ekranında hedeflemektedir. 
- Çalışma şekli aslında çok basit. Sadece uygulamayı çalıştırıyorsunuz ve birkaç saniye bekledikten sonra ürünlerin isimleri ve fiyatları konsol ekranına liste şeklinde geliyor ve listenin hemen alt tarafında fiyatların ortalamasını bulabilirsiniz.
- Çalışma şeklini baştan sona sıralamak gerekirse;
  * Uygulama çalışır çalışmaz girilen URL linkinin doğruluğunu kontrol ediyor ve internet sitesine bir "GET" HTTP metotu ile bir istek atarak sitenin HTML belgesini oluşturuyor.
  * Bu aşamadan sonra belge elimde olduğu için kaynak kodlarından istediğim verilerin bulunduğu bölmeleri nokta atışı belirliyorum ve bu bölmedeki bütün URL linklerini çekmesini istiyorum.
  * Elimde olan URL linkleri ile tekrardan sayfaların kaynak kodlarına ulaşıyorum ve bana lazım olan verileri çekiyorum. Çektiğim verileri istediğim formatta düzenleyerek önbellekte tutuyorum.
  * Verileri ardından bir liste şeklinde ürün ismi ve ürün fiyatı olmak üzere konsol ekranına yazdırıyorum ve hemen listenin altına fiyatların ortalamasını ekliyorum.
  * Konsol ekranına yazdığım liste gibi bir de masaüstüne .txt uzantılı dosya oluşturup aynı biçimde dosyanın içine de yazıyorum.
  * Ardından uygulamanın yapacağı bütün işlemler bitiyor.
- Uygulamada kullandığım kütüphaneler ve uygulamadaki amaçları;
  * **HtmlAgilityPack:** HTML belgelerini okumak ve işlemek için kullanılan bir kütüphanedir. Uygulamada kullanma amacım ise, internet sitesinin kaynak kodlarını okuyup istediğim verileri almak.
  * **RandomUserAgent:** Web dolaşmalarında ve bunun gibi uygulamalarda çok fazla istek durumunda olası IP banlanma ihtimaline karşı önlemler alınmalıdır. Bu önlemlerden en basiti ve en kullanışlı olanı istek atılırken kullanılan "UserAgent" başlığını her istekte farklı şekilde göndermektir. Bunun için uygulamamda her bir kaynak kodu çekmek için attığım isteklerde farklı bir başlık kullanmak adına isteği atmadan önce "UserAgent" başlığını uygun ve rastgele bilgilerle değiştirerek server tarafında farklı bir kullanıcı gibi görünmesini sağlayarak ihtimali oldukça düşürmeyi hedefliyorum. Web siteleri bu başlığı kullanarak tarayıcıları ve istemcileri tanıdığı için oldukça kullanışlı olduğuna inanıyorum.
