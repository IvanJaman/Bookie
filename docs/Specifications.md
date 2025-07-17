# App Specifications — User Roles & Permissions

## English Version

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
- Can **edit details** of books they added (optional).
- Can **view statistics** on books they published (optional).

#### 3. Admin

- Has all **User** and **Publisher** privileges.
- Can **delete books** from the system.
- Can **manage user accounts**:
  - Delete user accounts.
  - Modify user roles and permissions (optional).
- Can **moderate content**:
  - Delete comments and ratings.
  - Handle reports or complaints (if implemented).
- Oversees the **technical management** of the app.
- Has full **access to all administrative features**.

### Authentication & Account Management

- All users can **register**, **log in**, and **log out** securely.
- Account management includes:
  - Changing password.
  - Updating profile information.
  - Managing notification preferences (optional).
- Role-based authorization protects routes and features accordingly.

### Specific Functional Requirements by Role

| Feature                       | User | Publisher | Admin |
|-------------------------------|:----:|:---------:|:-----:|
| Register / Login              |  ✅  |    ✅     |  ✅   |
| Review books (comments, stars)|  ✅  |    ✅     |  ✅   |
| Manage book lists             |  ✅  |    ✅     |  ✅   |
| Add new books                |      |    ✅     |  ✅   |
| Edit books (own added)        |      | (Optional)|  ✅   |
| Delete books                 |      |           |  ✅   |
| Manage users (delete, roles)  |      |           |  ✅   |
| Moderate comments/ratings     |      |           |  ✅   |
| Access admin panel            |      |           |  ✅   |

---

# Specifikacije aplikacije — uloge korisnika i dopuštenja

## Hrvatska verzija

### Uloge korisnika i dopuštenja

#### 1. Korisnik

- Može se **registrirati i prijaviti** u sustav.
- Može **ocjenjivati knjige**:
  - Dodavati **komentare** na stranice knjiga.
  - Davati **zvjezdane ocjene** knjigama.
- Može **upravljati listama knjiga**:
  - Dodavati knjige na **gotove liste**, npr.:
    - Za pročitati (TBR)
    - Pročitano
  - Kreirati i uređivati **prilagođene liste** (npr. „Za kupiti“, „Omiljene“).
  - Uklanjati knjige s bilo koje liste.
- Može **uređivati svoje podatke računa**.

#### 2. Izdavač

- Ima sva dopuštenja **korisnika**.
- Osim toga, može **dodavati nove knjige** u sustav.
- Može **uređivati podatke** o knjigama koje je dodao (opcionalno).
- Može **gledati statistike** o knjigama koje je objavio (opcionalno).

#### 3. Administrator

- Ima sva dopuštenja **korisnika** i **izdavača**.
- Može **brisati knjige** iz sustava.
- Može **upravljati korisničkim računima**:
  - Brisati račune korisnika.
  - Mijenjati uloge i dopuštenja korisnika (opcionalno).
- Može **moderirati sadržaj**:
  - Brisati komentare i ocjene.
  - Rješavati prijave i žalbe (ako se implementira).
- Odgovoran je za **tehničko upravljanje** aplikacijom.
- Ima pristup svim **administrativnim funkcijama**.

### Autentikacija i upravljanje računima

- Svi korisnici se mogu **registrirati**, **prijaviti** i **odjaviti** sigurno.
- Upravljanje računom uključuje:
  - Promjenu lozinke.
  - Ažuriranje profila.
  - Upravljanje postavkama obavijesti (opcionalno).
- Zaštita funkcija i ruta temelji se na ulogama korisnika.
