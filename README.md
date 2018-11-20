
# DreamTeamProject
<div display="flex" justify-content="space-between">
  <img src="dotnet_logo.png" width="50">
  <h1>Aplikacja obsługi domowej biblioteki</h1>
</div>

<h2>Zagadnienia obejmujące zadanie:</h2>
<ul>
<li>Html5, CSS, Bootstrap i ASP MVC Core</li>
<li>ogowanie i rejestracja użytkowników</li>
<li> Składowanie i odczyt danych z bazy danych</li>
<li> Komunikacja sieciowa z usługami własnymi (usługa autentykacji) jak i zewnętrznymi (Azure Cognitive Services)</li>
<li> Azure Storage</li>
<li> Udostępnianie usług w Azure</li>
<li> Opcjonalnie: Angular5 + SPA</li>
</ul>


<h2>Technologie:</h2>
<ul>
<li> .Net MVC Core (Angular 5 w dalszej częśći jeśli pozwoli czas) </li>
<li> .Net Core + EntityFramework.Core</li>
<li> SQL Server (Wystarczy localdb)</li>
</ul>
<h2>Wymagania:</h2>
<ul>
<li> Aplikacja działa w trybie edycji dla zalogowanych użytkowników</li>
<li> Aplikacja działa w trybie odczytu dla użytkowników anonimowych</li>
<li> Tryb odczytu:</li>

<ul>
<li> Wyszukiwanie elementów bilbioteki:</li>
<li> Wyszukiwanie obiektów wg:</li>

<ul>
<li> Tytułu</li>
<li> Autora/Reżysera</li>
<li> Nośnika:</li>

<ul>
<li> DVD</li>
<li> CD</li>
<li> Książka</li>
</ul></ul>

<br><li> Lista wyników (każdy element tylko overview)</li>
<ul>
<li> Po kliknięciu w element widoczne szczegóły elementu</li>
</ul>

<li> Wyświetlenie detali elementu</li>

<br><li> Panel podglądu szczegółów elementu</li>

<ul>
<li> Pola do odczytu:</li>

<ul>
<li> Okładka lub placeholder</li>
<li> Tytuł</li>
<li> Rok</li>
<li> Nośnik</li>
<li> Nazwisko/Nazwa</li>
<li> Status</li>
</ul></ul>
<li> Formularz rejestracji nowego użytkownika</li>
<li> Formularz logowania </li>
</ul>

<br><li> Tryb edycji:</li>
<ul>
<li> Dodawanie nowych elementów do biblioteki</li>
<ul>
<li> Panel dodawania elementu do biblioteki:

<ul>
<li> Tytuł</li>
<li> Rok</li>
<li> Nośnik z listy</li>
<li> Nazwisko reżysera (tylko DVD)</li>
<li> Nazwisko autora (tylko książka)</li>
<li> Nazwa wykonawcy (tylko cd)</li>
<li> Opcjonalnie: okładka
</ul>

<li> Wykorzystać Usługi Azure Cognitive Services do rozpoznania tytułu utworu na podstawie zdjęcia</li>
</ul>


<li> Zmiana statusu elementu (na półce/wypożyczony)</li>

<ul>
<li> Zmiana statusu na wypożyczony wymaga podania w komentarza (w domyśle info do kogo)</li>
<li> Zmiana statusu na wypożyczony zapamiętuje datę wypożyczenia</li>
<li> Zmiana statusu na wypożyczony zapamiętuje kto dokonał zmiany statusu</li>
<li> Zmiana statusu na „na półce” kasuje powyższe informacje</li>
</ul>

<br><li> Podgląd statystyki (podstawowy raport):</li>

<ul>
<li> Ilość elementów ogółem</li>
<li> Ilość elementów per nośnik</li>
<li> Ilość dostępnych</li>
<li> Ilość niedostępnych </li>
<li> Lista niedostępnych elementów z uwzględnieniem kto i kiedy ustawił im status na niedostępny</li>
</ul></ul></ul>

<strong><italic>Dane tekstowe dotyczące każdego elementu biblioteki (Tytuł, Rok....) powinny być przechowywane w bazie danych SQL
Dane binarne np: obrazy powinny być przechowywane w Azure Blob Storage
Aplikacja powinna być wgrana na usługę Azure. </strong></italic>
