OISISI projekat - desktop aplikacija za referenta sajma knjiga



|Uloga | Ime i prezime | Broj indeksa|
|---|---|---|
|Student 1 | Lidija Vidic | RA213/2025|
|Student 2 | Anastasija Timotic | RA194/2023| 

## Struktura projekta

    SajamKnjiga.Core - modeli, serijalizacija, cuvanje u fajlove i DAO klase

    SajamKnjiga.CLI - konzolna aplikacija (meniji za sve entitete)

    Data - tekstualni fajlovi sa podacima, pravi se automatski pri prvom pokretanju


## Podela posla

## Lidija Vidic (Student 1)
- Kreiranje solution-a (Core i CLI projekti)
- Serijalizacija i čuvanje u fajlove: `ISerializable`, `Serializer`, `Storage`, `Putanje`, `CsvFormat`
- Posetilac: `Posetilac`, `StatusPosetioca`, `PosetilacDAO`, `PosetilacConsoleView`
- Kupovina: `Kupovina`, `KupovinaDAO`, `KupovinaConsoleView`, prosečna ocena posetioca
- Izdavač: `Izdavac`, `IzdavacDAO`, `IzdavacConsoleView` (provera šefa izdavača)
- Lista želja: `StavkaListeZelja`, `ListaZeljaDAO`
- Učitavanje i povezivanje izdavača i lista želja u `Program.cs`

##Anastasija Timotic (Student 2)
- Adresa, autor i knjiga: `Adresa`, `Autor`, `Knjiga`, `AutorKnjiga`
- `AutorDAO`, `KnjigaDAO` (veza autor–knjiga, kupci knjige)
- `ConsoleViewUtils` (unos podataka u konzoli)
- `AutorConsoleView`, `KnjigaConsoleView`
- Glavni meni i učitavanje podataka u `Program.cs`
- Referencijalni integritet pri brisanju autora i knjige




