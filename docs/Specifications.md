# App Specifications — User Roles & Permissions

### User Roles and Permissions

#### 1. User

- Can **register and log in** to the system.
- Can **review books** by:
  - Adding **comments** on book pages.
  - Giving **star ratings** to books.
- Can **manage book lists**:
  - Add books to **pre-made lists** such as:
    - To Be Read (TBR)
    - Read
  - Create and manage **custom lists** (e.g., “To-Buy”, “Favorites”).
  - Remove books from any lists.
- Can **edit their own account information**.

#### 2. Publisher

- Has all **User** privileges.
- Additionally, can **add new books** to the system.
- Can **edit details** of books they added.

#### 3. Admin

- Has all **User** and **Publisher** privileges.
- Can **delete books** from the system.
- Can **manage user accounts**:
  - Delete user accounts.
  - Modify user roles and permissions.
- Can **moderate content**:
  - Delete comments and ratings.
- Oversees the **technical management** of the app.
- Has full **access to all administrative features**.

### Authentication & Account Management

- All users can **register**, **log in**, and **log out** securely.
- Account management includes:
  - Changing password.
  - Updating profile information.
- Role-based authorization protects routes and features accordingly.

### Specific Functional Requirements by Role

| Feature                       | User | Publisher | Admin |
|-------------------------------|:----:|:---------:|:-----:|
| Register / Login              |  ✅  |    ✅     |  ✅   |
| Review books (comments, stars)|  ✅  |    ✅     |  ✅   |
| Manage book lists             |  ✅  |    ✅     |  ✅   |
| Add new books                |      |    ✅     |  ✅   |
| Edit books (own added)        |      | ✅ |  ✅   |
| Delete books                 |      |           |  ✅   |
| Manage users (delete, roles)  |      |           |  ✅   |
| Moderate comments/ratings     |      |           |  ✅   |
| Access admin panel            |      |           |  ✅   |

---

# Specifikacije aplikacije — uloge korisnika i dopuštenja

### Uloge korisnika i dopuštenja

#### 1. Korisnik

- Može se **registrirati i prijaviti** u sustav.
- Može **brojčano ocjenjivati knjige**:
  - Dodavati **pisane recenzije** na knjige.
  - Davati **zvjezdane ocjene** knjigama.
- Može **upravljati popisima knjiga**:
  - Dodavati knjige na **gotove popise**, npr.:
    - Za pročitati 
    - Pročitano
  - Kreirati i uređivati **prilagođene popise** (npr. „Za kupiti“, „Omiljene“).
  - Uklanjati knjige s bilo kojeg popisa.
- Može **uređivati  podatke svog računa**.

#### 2. Izdavač

- Ima sva dopuštenja **korisnika**.
- Osim toga, može **dodavati nove knjige** u sustav.
- Može **uređivati podatke** o knjigama koje je dodao.

#### 3. Administrator

- Ima sva dopuštenja **korisnika** i **izdavača**.
- Može **brisati knjige** iz sustava.
- Može **upravljati korisničkim računima**:
  - Brisati račune korisnika.
  - Mijenjati uloge i dopuštenja korisnika.
- Može **moderirati sadržaj**:
  - Brisati komentare i ocjene.
- Odgovoran je za **tehničko upravljanje** aplikacijom.
- Ima pristup svim **administrativnim funkcijama**.

### Autentikacija i upravljanje računima

- Svi korisnici se mogu **registrirati**, **prijaviti** i **odjaviti** sigurno.
- Upravljanje računom uključuje:
  - Promjenu lozinke.
  - Ažuriranje profila.
  - Upravljanje postavkama obavijesti (opcionalno).
- Zaštita funkcija i ruta temelji se na ulogama korisnika.
