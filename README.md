# AgriEnergyConnect 🌱

> A web-based prototype system for managing agricultural product listings and farmer profiles. This platform enables farmers to add and manage their own products, while employees can oversee and filter all records.

This README provides a comprehensive overview of the AgriEnergyConnect application, its features, and detailed instructions for setup and deployment.

---

### 📋 Table of Contents

*   [About The Project](#about-the-project)
*   [Key Features](#key-features)
*   [Technology Stack](#technology-stack)
*   [Video Demonstration](#-video-demonstration)
*   [Visual Walkthrough](#-visual-walkthrough)
*   [Getting Started](#-getting-started)
    *   [Prerequisites](#prerequisites)
    *   [Installation & Setup](#installation--setup)
*   [Usage](#-usage)
    *   [Running the Application](#running-the-application)
    *   [Test Credentials](#test-credentials)


---

## About The Project

AgriEnergyConnect is designed to help agricultural organisations manage relationships with farmers and their products. The system provides **secure, role-specific access** for two main user types: **Farmers** and **Employees**.

Farmers can manage their own product listings, while employees of the agricultural organisation have a centralized dashboard to view all products from all farmers, manage farmer profiles, and filter data as needed. The platform is built with a clean, responsive user interface to ensure a seamless experience on any device.

## Key Features

### 🧑‍🌾 For Farmers
*   Securely register an account and log in.
*   Add new agricultural products to the system.
*   View, edit, and delete **only their own** product listings.

### 🏢 For Employees
*   Securely log in to the employee dashboard.
*   Add new farmer profiles to the system.
*   View and manage **all products** from **all farmers**.
*   Filter the complete product list by:
    *   Category (e.g., Fruits, Vegetables)
    *   Production date range

### ⚙️ General
*   **Secure Role-Based Navigation:** The user interface dynamically changes based on the logged-in user's role.
*   **Responsive Design:** Styled with custom CSS and Bootstrap for full responsiveness on desktops, tablets, and mobile devices.
*   **Relational Database:** All data is managed using Entity Framework Core with a relational SQL Server database.

## Technology Stack

This project is built using the following technologies:

*   **Backend:** ASP.NET Core MVC (.NET 8.0)
*   **Database:** SQL Server (via LocalDB)
*   **ORM:** Entity Framework Core
*   **Frontend:** HTML, CSS, Bootstrap
*   **Development Environment:** Visual Studio 2022

---

## 🎥 Video Demonstration

For a quick overview and a live demo of the application's features, watch the video below. Click the thumbnail to play.

[![Watch the video](https://youtu.be/OrfeuxC1jDg?si=hwDtHi9NFMdrIapD)

---

## 📸 Visual Walkthrough

Here is a visual tour of the AgriEnergyConnect application's main features.

<details>
<summary><strong>1. Homepage, Login, and Registration</strong></summary>

**Homepage:** The public landing page for all visitors.
![Screenshot 2025-05-14 185153](https://github.com/user-attachments/assets/afaa35d5-58ed-4efe-9799-f9324c32a054)


**Login Page:** Secure login for existing users.
![Screenshot 2025-05-14 185209](https://github.com/user-attachments/assets/8e0fc6de-91c0-4946-b520-bf25692a91a6)

**Registration Page:** New users can create an account and select a "Farmer" or "Employee" role.
![Screenshot 2025-05-14 185248](https://github.com/user-attachments/assets/9c054458-840c-4f2f-9a5a-2dca6b444c57)
![Screenshot 2025-05-14 185321](https://github.com/user-attachments/assets/8d911e20-3ea2-4949-a0f2-34ef1127eb0e)

</details>

<details>
<summary><strong>2. Farmer Dashboard & Product Management</strong></summary>

**Farmer Dashboard:** After logging in, farmers are greeted with a personalized dashboard.
![Screenshot 2025-05-14 185626](https://github.com/user-attachments/assets/bd73c29e-1ed8-4436-ba96-40226d697ad2)

**Add Product Form:** Farmers can easily add new products with a name, category, and production date.
![Screenshot 2025-05-14 185654](https://github.com/user-attachments/assets/a7d69e9f-4b36-479c-a836-3eb0098d73bb)

**My Products List:** Farmers can view, edit, and delete their own products.
![Screenshot 2025-05-14 185716](https://github.com/user-attachments/assets/3f68659f-f80b-43d2-bc7e-b4c42a3b1eed)

</details>

<details>
<summary><strong>3. Employee Dashboard & Management</strong></summary>

**Employee Dashboard:** The central hub for employees to manage farmers and products.
![Screenshot 2025-05-14 185413](https://github.com/user-attachments/assets/56d3b7ea-d2ec-4377-8073-d919c922a87d)

**Farmer Profiles:** Employees can view a list of all farmers, with options to edit or delete their profiles.
![Screenshot 2025-05-14 185446](https://github.com/user-attachments/assets/05c815aa-fbef-4f9f-af46-b675fdbce152)

**Product List & Filtering:** Employees can view all products and use powerful filters to narrow down the results by category and date.
![Screenshot 2025-05-14 185509](https://github.com/user-attachments/assets/e28f2c42-e028-4430-8915-32b253fd4073)
![Screenshot 2025-05-14 185535](https://github.com/user-attachments/assets/e3fcf64e-d1b6-4aef-9600-20c79474e9c9)

</details>

---

## 🚀 Getting Started

Follow these instructions to get a local copy of the project up and running on your machine for development and testing purposes.

### Prerequisites

Ensure you have the following software installed:

*   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
*   [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
*   [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB (usually installed with Visual Studio)

### Installation & Setup

1.  **Clone the Repository**
    Open your command line or terminal and run the following command:
    ```sh
    git clone https://github.com/paayalrakesh/AgriEnergyConnect.git
    ```

2.  **Open the Project in Visual Studio**
    Navigate to the cloned folder and open the `AgriEnergyConnect.sln` file.

3.  **Configure the Database Connection**
    Open the `appsettings.json` file. The default connection string is configured to use SQL Server LocalDB. If you are using LocalDB, no changes are needed.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AgriEnergyDB;Trusted_Connection=True;"
    }
    ```

4.  **Apply Database Migrations and Seed Data**
    This step creates the database schema and populates it with initial roles and test users.
    *   In Visual Studio, go to `Tools` -> `NuGet Package Manager` -> `Package Manager Console`.
    *   Run the following commands one by one:
    ```powershell
    Add-Migration InitialCreate
    Update-Database
    ```
    This will create the `AgriEnergyDB` database and its tables.

## 💻 Usage

### Running the Application

1.  Press **`F5`** or click the **Start** button (with the green play icon) in Visual Studio.
2.  Your web browser will open, and the site will launch at a local address, such as `https://localhost:xxxx`.

### Test Credentials

You can use the pre-seeded user accounts below to test the role-specific features. You can also register new users.

| Role      | Username | Password |
| :-------- | :------- | :------- |
| Farmer    | `James`  | `HarryP` |
| Employee  | `Lilly`  | `Harry`  |


---

*Last Updated: June 2025*

Made with ❤️ for improving agricultural efficiency.
