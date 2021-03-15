# ShopApp
.Net Core 3.0 ile Çok Katmanlı Mimari yapısını kullanarak örnek bir E-Ticaret Projesi geliştirdim.Bu projede kullanıcı hesap oluşturabilir,sepetine ürün ekleyebilir ve sipariş verebilir.Projede 4 katman mevcuttur.

## Entities Katmanı
Bu katmanda projemizde kullanacağımız veri sınıflarının tanımlamalarının yapıldığı katmandır.Çok katmanlı mimarinin en temel katmanıdır.Örneğin,Product,Category,Order,Cart vb.

## DataAccess Katmanı
Bu katmanda Entities Katmanındaki sınıflar kullanılarak Database üzerinden ve Database üzerine işlemlerin yapıldığı katmandır.Örneğin ürün ekleme,ürün silme,sipariş oluşturma vb.

## Business Katmanı
Bu katmanda DataAccess Katmanındaki işlemlerden ziyade verilerin istenildiği gibi kullanmayı sağlayan katmandır.Örneğin personel eklerken numarasının 11 hane olmasının özelleştirilebilir

## WebUI
Projemizin kullanıcı kısmına gösteriminin yapıldığı katmandır ve bu katmanda Model View Controller yapısı kullanılmıştır.Örneğin bu katman da sayfaları tasarlayıp yönetebilirsiniz.
