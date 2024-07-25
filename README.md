Hello,
This is My Financial_App second Round interview exercise
This is .Net 8 wep Api that uses JWT/Identity authentication/authorization
Uses PostgreSQL as Database(might need to adjust Connection String,JWT Audience and Issuer in appsettings)
It is built using Clean Architecture
there are in total 11 endpoints
Api/Auth/Login   



excpects userName and password
Api/Auth/Register 






/Api/Client/Get


/Api/Client/GetListWithSearchEngine


/Api/Client/Create  postman Body/form-data



/Api/Client/Update     postman Body/form-data




/Api/Client/Delete

/api/SearchEngine/Get



/api/SearchEngine/Suggestions



/api/SearchEngine/Create



/api/SearchEngine/Update


Client and SearchEngine controller are only usable for Admin users(when Registering be sure to Provide "Admin" as role)
Client/Update and Client/Create are built in a way that expects to be called through postMan,
Code is Easy to navigate,Starting Project is Financial_App
Controllers are in Presentation Folder,Which references and calls Application layer after mapping 
Presentation/Model files to Domain/Entities models,Application layer uses files from Infrastructure
as helpers,then comes Persistance layer(for database Configurations and Interfaces)


Used Libraries and Packages
Mediatr,     Automapper,      EntityFrameworkCore,      libPhoneNumbers
