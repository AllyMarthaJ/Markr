# Markr
Markr Exam Analysis HTTP Microservice

# Assumptions
Assumptions: Windows only server right now (Docker requires Windows with the current build, hazzah! -- .NET 6 & ASP.NET support Linux, so easy to change for future). 
Access to local file storage is a BIG must. (SQLite file storage)
Designed to all specifications in Markr brief. 

# Requirements
- Optional: SQLite 
- ASP.NET Core Runtime / .NET 6 (via docker-compose)
- Docker

# Approach
I used ASP.NET via .NET 5 and then migrated to .NET 6 half way through due to compat issues. SQLite was used to facilitate persistent storage, and
ASP.NET was used as the driving framework, via C#. 80% of time was spent reading documentation, 20% of the time was used to code. SQLite was facilitated
via Entity Framework so as to make driving the db easier. NUnit was used to drive unit tests.

Previous reading into ASP.NET and SQLite was done; previous knowledge of .NET was used to drive implementation. Still, countless unforeseen issues with software
including Docker forced more research at the time of development -- hence increasing development time. 

(I had to learn the following technologies: parts of ASP.NET Core alongside data serialization in ASP.NET (which does it for you!), EF, and Docker. 
Mid-project I had to migrate from .NET 5 to .NET 6 in order to install EF6 for SQLite support. 
Furthermore, I had software issues with docker, windows, visual studio. 
Seriously I should just consider Linux at this point, like my benevolent girlfriend has been saying all along. 
I estimate approximately 80% of my time was looking at documentation and examples.)

All in all, ~4 hours spent on this task (3-5 hours given), most of which I spent on research time.

# Installation/Running
1. Install Docker by any means necessary (enslave a junior developer, if I might suggest).
2. Download/pull this repo and open a terminal in the main directory with `docker-compose.yml`.
3. Run `docker-compose up`; wait for installation of .NET 6 to commence.
4. A browser should open when complete to API documentation, as given in the spec. Follows OpenAPI via Swagger.
