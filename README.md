# İçindekiler
* [Takım Adı](#takım-adı)
* [Oyun ile İlgili Bilgiler](#oyun-ile-ilgili-bilgiler)
* [Game Design Documantation](#game-design-documantation)
* [Sprint 1](#sprint-1)
* [Sprint 2](#sprint-2)
* [Sprint 3](#sprint-3)
* [Kullanılan Assetler/Eklentiler](#kullanılan-assetlereklentiler)

## Takım Adı
Takım Unity U-49

## Oyun ile İlgili Bilgiler
* ***İzometrik, Aksiyon-Macera***
* ***Windows (PC)***
  
### Takım Elemanları
* **Ali Yağmur:** *Product Owner - Developer*
* **Safiye İrem Turna:** *Scrum Master - Developer*
* **Nebi Cihan Akpınar:** *Developer - Sound Director*
* **Sidar Aygaz:** *Developer - Art Director*
* **Yunus Kale:** *Developer - UI Director*

### Diyalog Seslendirmeleri
* **The Witch:** *Safiye İrem Turna*
* **The Necromancer:** *Sidar Aygaz*
* **Henric the Ghost:** *Ali Yağmur*

### Oyun Adı
The Witch of Shunrald

### Product Backlog URL
[Product Backlog board](https://sharing.clickup.com/9009152983/b/h/8cftgyq-1160/925c305e7121a0b)

### Oyun Açıklaması
   The Witch of Shunrald, hikâye anlatım unsurlarını barındıran bir izometrik aksiyon macera oyunudur. Geliştirdiğimiz bilgisayar oyununun ana mekaniği oyuncunun fare kontrolleri ile cadının asasını doğrudan kontrol etmesi ve bu şekilde menzilli veya yakın dövüş saldırıları yapabilmesidir. Ayrıca oyuncu etrafta bulunan bazı nesneleri de fare hareketleri ile oynatıp konumlandırabilir. Bir cadı rolünü üstlenen oyuncu esrarengiz bazı olayları inceler ve bulduğu izleri takip ederek bir mağaraya ulaşır. Olayların sorumlusu olan Necromancer’ı bulup durdurmak oyuncunun görevidir. 

### Oyun Özellikleri
* İzometrik aksiyon macera oyunu
* Fare girdilerini bire bir takip edebilen özgün bir saldırı mekaniği. Bu mekanik ile oyuncu savaşmak için kuşandığı silahı doğrudan kendisi kullanıyor gibi hissedecek.
* Doğrudan fare girdileri ile manipule edilebilir çevre elementleri (Örneğin kapıların oyuncu tarafından tutulup sürüklenerek açılması.)
* Sürükleyici ve merak uyandıran bir hikaye
* Atmosfere uygun ses ve müzik
* Zorlayıcı "Boss Fight" sahneleri

### Hedef Kitle
* **Aksiyon-Macera Hayranları:** Aksiyon, keşif ve hikaye anlatımının karışımını içeren sürükleyici oyun deneyimlerini seven oyuncular.

* **Fantazi ve Doğaüstü Severler:** Cadılar, büyücüler ve doğaüstü yetenekler gibi konular oyunun fantastik ve doğaüstü türlerine ilgi duyan oyuncuları cezbedecektir.

* **Benzersiz Mekaniklere İlgi Duyan Oyuncular:** Oyunun fare tabanlı kontrollere odaklanması, cadının asasını kullanma ve nesnelerle etkileşim kurma gibi yenilikçi ve hassas kontrol mekaniklerini takdir eden oyuncuları cezbedecektir.

#### Juriye Not
* *Ekibimiz beş developerdan oluşmaktadır ve aramızda herhangi bir designer/artist bulunmamaktadır. Bu nedenle 3D modellerde ücretsiz asset'lerden faydalandık. Kullanılan assetlere [Kullanılan Assetler/Eklentiler](#kullanılan-assetlereklentiler)'den ulaşılabilir.*
* *İlk Sprint'te sınav haftasından dolayı daha çok pre-production kısmına yer verdik.*
* *Sprint puanlamalarında belli bir yerden sonra Click-Up para talep ettiği için puanlamaları backlog'daki task'ların önünde belirtmek durumunda kaldık.*
* *The Witch, asırlar önce yaptığı bir büyü savaşında kollarını/ellerini kullanma yetisini kaybetmiştir. O nedenle karakterimizin kolları rigidbody - fizik sayesinde hareket etmektedir. Oluşan görüntü animasyona bağlı bir bug olmamakla beraber Animation Rigging - Unity kullanılarak özenle yapılmıştır.*
* *Aynı şekilde, Weeping Angel mini boss karakteri Doctor Who (DW)'deki Ağlayan Melek'lerden esinlenerek yapılmıştır. Melek heykelleri, göz kırptığımızda yani görüş alanımızdan çıktığında bize doğru hareket eder, onlara doğru döndüğümüzde ise (mouse imleci takibi) taşlaşırlar; tek dokunuşları bizi taşlaştırıp öldürmeye yetecek güçtedir.*

## Game Design Documantation
[GDD](https://doc.clickup.com/9009152983/d/h/8cftgyq-1720/77fc25bd1accf24)

## Sprint 1
* **Sprint Notları:** User Story'ler (description alanında) ve Kabul Kriterleri (subtask olarak) Product Backlog'ların içine yazılmıştır. Product backlog item'lara tıklandığında hikayelerin detayları ve kabul kriterleri okunabilir.

* **Sprint içinde tamamlanması tahmin edilen puan:** 41 Puan

* **Puan tamamlama mantığı:** Toplamda proje boyunca tamamlanması gereken 356 puanlık backlog bulunmaktadır. İlk Sprint'te daha çok pre-production kısmına yer verildiği için 41 puanlık bir hedef koymaya karar kıldık. 41 puana başarılı bir şekilde ulaştık ve geriye 315 puanımız kaldı.

* **Backlog düzeni ve Story seçimleri:** Backlog'umuz *"must have, should have, could have, would have"* şeklinde düzenlenmiştir. Sprint başına tahmin edilen puan sayısını geçmeyecek şekilde sıradan seçimler yapılmaktadır. Seçimlerimiz, ekip arasında alınan kararlar doğrultusunda pre-pruduction'u kapsayacak şekilde ayarlanmıştır.

*Story'ler yapılacak işlere (task'lere) bölünmüştür. ClickUp Board'da görünen backlog item'ların içerisinde, description kısmında story'ler yer alırken, subtask'lerde ise kabul kriterleri yer almaktadır.*

* **Daily Scrum:** Daily Scrum toplantıları Discord kanalı üzerinden gün aşırı yapılmıştır. Toplantıda alınan kararlara ve paylaşımlara ekte yer verilmiştir.

   *[Sprint 1 Daily Scrum Chats](https://doc.clickup.com/9009152983/d/h/8cftgyq-1760/386829abde41433)*

* **Sprint board update:** ***Sprint board screenshotları:***

   ![FirstSprint_1](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/ea78319c-6f12-4ebe-a22e-86d8f2a2c678)
   ![FirstSprint_2](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/b0cd5960-3e5a-4987-af2f-e62141fc3665)

* **Oyun Durumu:** Video:

   https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/445f15fc-9b4c-436b-80ce-ce9372442f85

   https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/013e57ca-76ab-493c-af99-b89d2a07ef89
  
* **Sprint Review:**
  * ***Alınan kararlar:***
    * *Projemiz için herkes Unity ortak sürümünü ve WebGL'i indirmiş olmalıdır.*
    * *Proje GitHub'dan çekilmiş ve hazır bir şekilde olmalıdır.*
    * *Proje'de tek bir ana sahne kullanılacaktır ve bu sahneye son eklemeler aktarılacaktır. Tüm ekim üyeleri için ayrı birer sahne oluşturulmuştur.*
    * *Ekibe Git Desktop'ta commit atmak ve Unity Engine içerisinde package export etmekle ilgili kısa bir anlatı yapılmış olup, tutarlılık adına bir [team guide](https://doc.clickup.com/9009152983/d/h/8cftgyq-2060/f078c65bbbd3ac9) oluşturulmuştur.*
    * *İlk Sprint'te daha çok pre-production kısmına yer verilmiş olup; görev atamaları yapılmış, oyun fikri üzerine tartışılmış, GDD (Game Design Documentation) oluşturulmuş ve Product Backloglar kullanıcı hikayeleri ve kabul kriterleri doğrultusunda oluşturulmuştur.*
    * *Kullanılacak assetler için bir asset havuzu oluşturulmuş ve demo scene'lerde denemeler yapılıp ekibe gösterilmiştir.*
    * *Hikaye ve oynanışa dair kararlar verilmiş olup, çevre design için bir harita oluşturulmuştur.*

   * ***Sprint Review katılımcıları:***
     * ***Ali Yağmur:*** *Product Owner - Developer*
     * ***Safiye İrem Turna:*** *Scrum Master - Developer*

* **Sprint Retrospective:**
  * Takım içi iletişimin daha kolay ve etkili bir şekilde sağlanması için yazılı kanalları kullanmaya özen gösterme kararı alınmıştır.
  * İlk Sprint'te gerek yoğunluğumuzdan, gerekse pre-production ağırlıklı task'lerden dolayı kod açısından çok aktif değildik, bu durumu ikinci Sprint ile birlikte backloglar üzerinden ekip üyelerinin yetenekleri ve istekleri doğrultusunda yapılacak olan görev atamaları ile daha aktif bir biçimde ele alma kararı aldık.
  * İlk Sprint'te hedeflerimizi tamamlamış olup, ekip içerisinde iyi bir sinerji yakaladık ve bunun devamı için elimizden geleni yapacağız.
  * Birinci Sprint'te yaptığımız görev dağılımı sonucunda atanan rolleri korumaya karar aldık. Buna ek olarak ekip üyemiz Sidar Aygaz'ı Art Director olarak atamaya karar verdik.
  * Bu organizasyonun mutlaka eğlenceli bir şekilde geçtiği sürece verimli olabileceğini, dolayısıyla görevlerimizi yerine getirmek adına bunalacak hale gelmemizin mantıklı olmadığı konusunda hemfikir olduk.

### Product Backlog URL
[First Sprint board](https://sharing.clickup.com/9009152983/b/h/6-900901695256-2/c0293ef8cd93852)

## Sprint 2
* **Sprint Notları:** Ekip üyelerinin uyumlu ve efektif çalışması sonucu başarılı bir sprint geçirildi. Daily scrum'lar günlük olarak Discord kanalımızdaki'daki "Daily Scrum - text" bölümünden yapıldı (ekran görüntüleri ekte sunulmaktadır).
  
* **Sprint içinde tamamlanması tahmin edilen puan:** 148 Puan

* **Puan tamamlama mantığı:** Toplamda proje boyunca tamamlanması gereken 356 puanlık backlog bulunmaktadır. Bunun 41 puanlık kısmı İlk Sprint'te tamamlanmıştır. İkinci Sprint'te daha çok production kısmına yer verildiği için 148 puanlık bir hedef koymaya karar kılınmıştır. Production kısmında ana ve yan (side) mekanikler, level sistemleri, prefablemeler ve hatta post-production'dan AudioManager kısmı da başarılı bir şekilde tamamlanmıştır. 356 puanın 148'ine hedeflenen şekilde ulaşılmış olup, geriye Üçüncü Sprint'te tamamlanmak üzere 167 puan kalmıştır.

* **Backlog düzeni:** Backlog'da yer alan task'lar daha çok production ve post-production'ı barındıracak şekilde oluşturuldu ve düzenlendi. Production'a ait task'lar ikinci sprinte atandı ve puanlamalar düzenlendi. Product Backlog task'ları sprintlere atandığından, gerekli detaylara eklerde sunulan sprint linkleri ve backlog url üzerinden ulaşılabilir.

* **Daily Scrum:** Daily Scrum toplantıları "Daily Scrum - text" adlı Discord kanalımız üzerinden günlük olarak yapılmış, ekip üyelerinin gidişatı ve koordinasyonu sağlanmıştır. Conflict yaşamamak adına pull-push esnasında haberleşme sağlanmış olup, proje sorunsuz bir şekilde ilerletilmiştir. Scrum kapsamında herkes "dün ne yaptım ve bugün ne yapacağım" şeklinde bilgilendirme yapmış olup, diğer ekip üyelerini güncel bir şekilde haberdar etmişlerdir. Bu sayede birbirine bağlı task'lere zamanında ve sorunsuz başlanmıştır. Daily Scrum yazışmalarına ekte yer verilmiştir.

   *[Sprint 2 Daily Scrum Chats](https://doc.clickup.com/9009152983/d/h/8cftgyq-2080/4640c02854d7f5a)*

* **Sprint board update:** ***Sprint board screenshotları:***

  ![SecondSprint_1](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/75ba49c1-1dca-47eb-9940-9f55d19bec46)

  ![SecondSprint_2](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/a9368c9f-f9cb-4250-90b4-4fa4313e0ced)

  ![SecondSprint_3](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/00877467-7ce9-44b1-aaa5-f9efad4d2052)

  ![SecondSprint_4](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/563988a1-3f31-46dc-8de7-69ae8aaad73c)

* **Oyun Durumu:** Video:

  * *Mekanikler ve oynanışın daha geniş bir perspektiften sunulabilmesi adına (10 mb sınırından ötürü) videoların görüntü kalitesi düşürülmüştür.*


   https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/e9350758-b85f-4832-8134-7da282cdcce5

   https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/fad2f925-ee4a-4931-9dd5-5fba28dafbe1

   https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/0a4db472-a6f3-4c9c-8f6c-9d2698fd8662

* **Sprint Review:**

    * İlk Sprint'te hazırlanan asset havuzundan assetler seçilmiş ve oyuna implement edilmişitir.
    * GameManager, LevelManager, UIManager ve AudioManager gibi Singleton'lar hazırlanmıştır.
    * Level1 ve Level2 sahneleri oluşturulmutur ve oynanabilir hale getirilmiştir.
    * Hedeflediğimiz şekilde karakter hareketi, asa kullanımı, boss mekanikleri ve enemyler gibi ana mekaniklerin yanı sıra dash, mana ve health bar gibi side mekanikler de tamamlanmıştır.
    * İkinci Sprint daha çok production task'ları ile ilerlemiş olup, post-production'a atanan AudioManager'da bu sprint'te tamamlanmıştır.
    * Ortak task'lardaki entegrasyon işlemleri tamamlanmış olup, 3.sprint'te polish kısmına geçmek hedeflenmiştir.

* **Sprint Retrospective:**

  * İkinci Sprint üzerine "neleri iyi yaptık, neleri daha iyi yapabilirdik" şeklinde sohbet havasında bir ekip tartışması yapıldı.
  * Ekibin her üyesi üzerine düşen görevi hakkıyla yapmış olup en büyük kazancın; ekip çalışması, ekip içi uyum ve iletişim, soru sorma-cevap alma rahatlığı ve yardımlaşmayı sağlayan güven ortamı olduğu üzerine ortak bir kanıyta varıldı.
  * Üçüncü Sprint'te ilk hafta kalan task'lerin üzerine düşülmesi, son haftanın ise daha çok gameplay, bug-fix, oynanabilirlik ve oyun dengesi üzerine geçmesi hedeflendi.
  * Üçüncü Sprint için Backlog üzerinden task ataması her ekip üyesine eşit koşullarda olacak şekilde yapıldı ve herkesin fikri alındı.
  * Görev dağılımında herhangi bir değişikliğe gidilmemesi şeklinde ortak kanıya varıldı.

### Product Backlog URL
[Second Sprint board](https://sharing.clickup.com/9009152983/b/h/6-900901695292-2/2dfa8afc30d6b3c)

## Sprint 3
* **Sprint Notları:** Son sprint'te daha çok bug-fix, tutorial paneli, diyalog sistemi, seslendirmeler, level'ları bağlama, Game Analytics (GA) kurulumu, server ile arena mücadelesi, artificial intelligence (AI) ile intro ve karakter görselleri oluşturulması, game play videosu ve webgl ile build alma üzerine çalışıldı. Görevler ekip üyeleri ile beraber başarılı bir şekilde tamamlandı ve ortaya oynanabilir bir oyuna ek olarak;
tutorial ve diyalog sistemleri, GA kurulumu ve server üzerinden çekilen bilgiler ile bir arena mücadelesi oluşturulmuştur; serverda haftalık yapılacak değişiklikler ile her hafta farklı bir mücadele ortamı oluşturulacaktır, oyuncu bu mücadelelerde gösterdiği başarı ışığında farklı "Badge" göstergeleri kazanacaktır.

* **Sprint içinde tamamlanması tahmin edilen puan:** 167 Puan

* **Puan tamamlama mantığı:** Toplamda proje boyunca tamamlanması gereken 356 puanlık backlog bulunmaktadır. Bunun 41 puanlık kısmı İlk Sprint'te, 148 puanı ise İkinci Sprint'te tamamlanmıştır. Üçüncü Sprint daha çok post-production ile geçmiş olup; bug-fix, polish, diyalog-tutorial sistem, GA, arena sistemi ve intro hazırlıklarına ayrılmıştır. 356 puanın 167'sine hedeflenen şekilde ulaşılmış olup, geriye tamamlanmayan bir puan açığı kalmamıştır.

* **Backlog düzeni:** Backlog'da yer alan task'lar daha çok post-production'ı barındıracak şekilde oluşturuldu ve düzenlendi. Post-production'a ait task'lar üçüncü sprinte atandı ve puanlamalar düzenlendi. Product Backlog task'ları sprintlere atandığından, gerekli detaylara eklerde sunulan sprint linkleri ve backlog url üzerinden ulaşılabilir.

* **Daily Scrum:** Daily Scrum toplantıları "Daily Scrum - text" adlı Discord kanalımız üzerinden günlük olarak yapılmış, ekip üyelerinin gidişatı ve koordinasyonu sağlanmıştır. Conflict yaşamamak adına pull-push esnasında haberleşme sağlanmış olup, proje sorunsuz bir şekilde ilerletilmiştir. Scrum kapsamında herkes "dün ne yaptım ve bugün ne yapacağım" şeklinde bilgilendirme yapmış olup, diğer ekip üyelerini güncel bir şekilde haberdar etmişlerdir. Bu sayede her bir task zamanında ve sorunsuz bir şekilde tamamlanmıştır. Daily Scrum yazışmalarına ekte yer verilmiştir.

   *[Sprint 3 Daily Scrum Chats](https://doc.clickup.com/9009152983/d/h/8cftgyq-2100/8e66c04ef1a670a)*

* **Sprint board update:** ***Sprint board screenshotları:***

  ![ThirdSprint_1](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/90a94d36-1e03-4bd5-add5-0b9c7de0322f)

  ![ThirdSprint_2](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/b3616494-f563-4c8a-bad1-2e6d40148db4)

  ![ThirdSprint_3](https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/93aa2432-2830-42f0-99a7-e51f2521f14d)

* **Oyun Durumu:** Video:
  
  1. ***Tutorial:*** *WASD, asa hareketi (Lv1)*
  
     https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/9d5ba986-e027-4d87-8352-c51624cfd0e5
     
  2.  ***Tutorial:*** *Dash, E skill (Lv1)*
     
     https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/5a5f37dc-107f-4db3-ba9c-e6ca84b04241
  
  3.  ***Tutorial:*** *Telekinezi, asa-kılıç değişimi (Lv2)*
 
     https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/ee18119e-dd20-4c84-bf29-edd79c612cf4
 
  4. ***Diyalog:*** *The Witch, Henric The Ghost, The Necromancer (Lv3)*
 
     https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/0f992880-5f6d-4dd9-bcff-b47acd5fb928
     
  5. ***MiniBoss:*** *Ağlayan Melekler (Lv4)*
     * *Ağlayan Melek metaforu Doctor Who'dan alınmıştır. Heykeller sen onlara bakmadığın anda, yani yönün onlara dönük değilken seni takip eder ancak ona baktığında taşlaşır ve tek dokunuşuyla seni taşa döndürüp öldürür.*

       https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/b01bad92-4e99-4d52-97a8-7ba59c8ecab8

  7. ***BossFight:*** *The Necromancer (Lv5)*
     * *Cadımız asırlar önceki bir savaşta ellerini hareket ettirme yatisini kaybetmiştir, o nedenle elleri fizik ile hareket etmekte/sallanmaktadır.*

       https://github.com/iraska/OUABootcamp-Unity_U49/assets/105501017/2e87b3f4-4cac-4421-9688-792139a13019
     
* **Sprint Review:**

    * Oyundaki ana ve side mekanikler polishlenmiştir.
    * Görsel bütünlüğü sağlamak adına levellar revize edilmiştir.
    * UI panele ek; tutorial panel, diyalog panel, arena - badges ve upgrade panel eklenmiştir.
    * Oyununumuzun sesleri özenle seçilmiş ve diyalog seslendirmeleri ekip üyeleri tarafından yapılmıştır.
    * Intro sahnesi ve karakter görselleri için AI'dan faydalanılmıştır.
    * Hikaye ve olay örgüsü GDD'ye sadık kalınarak ilerletilmiştir.
    * Gerekli GA kurulumları başarıyla yapılmıştır.
    * Server üzerinden çekilen bilgiler ile bir arena mücadelesi oluşturulmuştur; serverda haftalık yapılacak değişiklikler ile her hafta farklı bir mücadele ortamı oluşturulacaktır, oyuncu bu mücadelelerde gösterdiği başarı ışığında farklı "Badge" göstergeleri kazanacaktır.
    * Tüm bunların sonucunda oyunumuz, designer/artist eksiğimize rağmen görsel bütünlüğe sahip; oynanabilir ve eğlenceli bir hale getirilmiştir.

* **Sprint Retrospective:**

  * Son Sprint Retrospective'imizde ekip içi başarı ve birlik adına kutlama yapılmış olup, ekip üyeleri; uyum, bağlılık, çalışma azmi ve görev bilinci adına birbirlerine teşekkür etmişlerdir.
  * Oyun başarıyla baştan sona oynanabilir ve keyifli bir hale büründürülmüş olup, ekip üyeleri ile beraber Discord üzerinden The Witch of Shunrald'ı oynama etkinliği yapılmıştır.
    
### Product Backlog URL
[Third Sprint board](https://sharing.clickup.com/9009152983/b/h/6-900901695294-2/3b36b46f61e3d56)

## Kullanılan Assetler/Eklentiler
[Click-Up Documentation](https://doc.clickup.com/9009152983/d/h/8cftgyq-2040/5d75aed4012a6c9)
