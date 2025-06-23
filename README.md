# CQRS Pattern Örnek Projesi

Bu proje, ASP.NET Core kullanılarak **CQRS (Command and Query Responsibility Segregation)** tasarım deseninin nasıl uygulanacağını gösteren bir örnek uygulamadır. Proje, CQRS desenini MediatR kütüphanesi ile birleştirerek modern ve ölçeklenebilir bir API yapısı sunar.

## Projenin Amacı

Projenin temel amacı, bir sistemdeki veri okuma (Query) ve veri yazma (Command) operasyonlarının sorumluluklarını birbirinden ayırmaktır. Bu ayrım, aşağıdaki avantajları sağlar:

* **Ölçeklenebilirlik:** Okuma ve yazma işlemleri için farklı optimizasyonlar ve veritabanları kullanılabilir.
* **Performans:** Okuma işlemleri, sorguya özel optimize edilmiş modeller üzerinden daha hızlı yapılabilir.
* **Esneklik ve Bakım Kolaylığı:** Sistemin karmaşıklığı azalır, kodun yönetimi ve yeni özellik eklemek kolaylaşır.

## Temel Kavramlar

* **Command:** Sistemin durumunu değiştirmeye yönelik niyet belirten nesnelerdir (Örn: `CreateProductCommand`). Genellikle bir değer döndürmezler.
* **Query:** Sistemden veri okumaya yönelik niyet belirten nesnelerdir (Örn: `GetProductByIdQuery`). Her zaman bir veri döndürürler.
* **Handler:** Her bir Command veya Query'yi işleyen, asıl iş mantığını içeren sınıflardır.
* **Mediator Deseni:** `Command/Query` nesnelerini ilgili `Handler`'lara yönlendiren bir aracıdır. Bu proje, bu deseni uygulamak için popüler **MediatR** kütüphanesini kullanır. Bu sayede `Controller` katmanı ile iş mantığı arasındaki bağımlılık (coupling) en aza indirilir.

## Nasıl Çalışır?

1.  **İstek Gelir:** İstemciden (Client) API Controller'a bir HTTP isteği gelir.
2.  **Nesne Oluşturulur:**
    * Eğer istek bir yazma işlemi ise (POST, PUT, DELETE), Controller bir **Command** nesnesi oluşturur.
    * Eğer istek bir okuma işlemi ise (GET), Controller bir **Query** nesnesi oluşturur.
3.  **MediatR'a Gönderilir:** Oluşturulan `Command` veya `Query` nesnesi, `MediatR` aracılığıyla işlenmek üzere gönderilir (`mediator.Send(...)`).
4.  **Handler Bulunur ve Çalıştırılır:** `MediatR`, gelen nesnenin türüne göre ilgili `Handler`'ı bulur ve çalıştırır.
5.  **İş Mantığı Yürütülür:** `Handler`, veritabanı işlemleri dahil olmak üzere gerekli tüm iş mantığını yürütür.
6.  **Sonuç Döner:**
    * `QueryHandler`'lar, elde ettikleri veriyi Controller'a geri döndürür.
    * `CommandHandler`'lar genellikle işlem sonucunda bir `Id` veya başarı durumu döner.

## Proje Yapısı

Proje, CQRS deseninin temel bileşenlerini yansıtacak şekilde yapılandırılmıştır:

* **WebApi:** ASP.NET Core projesidir. Controller'ları barındırır.
* **Application Katmanı:**
    * **Commands:** Yazma operasyonlarının nesnelerini ve `Handler`'larını içerir.
    * **Queries:** Okuma operasyonlarının nesnelerini ve `Handler`'larını içerir.
* **Persistence/Infrastructure Katmanı:** Veritabanı bağlantısı, Entity Framework context'i ve migration'lar gibi altyapısal kodları içerir.

## Kullanılan Teknolojiler

* **Backend:** ASP.NET Core
* **Mimari Desen:** CQRS (Command and Query Responsibility Segregation)
* **Aracı Kütüphane:** MediatR
* **Veri Erişimi:** Entity Framework Core

## Kurulum ve Çalıştırma

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:

1.  **Repoyu Klonlayın:**
    ```sh
    git clone [https://github.com/SametDulger/CQRS.git](https://github.com/SametDulger/CQRS.git)
    ```

2.  **Proje Dizinine Gidin:**
    ```sh
    cd CQRS
    ```

3.  **Veritabanı Bağlantısını Yapılandırın:**
    `WebApi` projesindeki `appsettings.json` dosyasında bulunan veritabanı bağlantı dizesini (Connection String) kendi yerel veritabanınıza göre güncelleyin.

4.  **Veritabanını Oluşturun (Migrations):**
    Package Manager Console üzerinden veya `dotnet cli` kullanarak veritabanı migration'larını çalıştırın.
    ```sh
    # Projenin ana dizinindeyken
    dotnet ef database update --project YourPersistenceProjectName
    ```
    *(Not: `YourPersistenceProjectName` kısmını reponuzdaki altyapı projesinin adıyla değiştirin.)*

5.  **Uygulamayı Çalıştırın:**
    `WebApi` projesini Visual Studio üzerinden başlatın veya aşağıdaki komutu kullanın:
    ```sh
    dotnet run --project WebApi/WebApi.csproj
    ```
