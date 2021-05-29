# n-layers-architecture project structure

This is a .NET core API project structure built follow the [traditional n-layers architecture](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#traditional-n-layer-architecture-applications "traditional n-layers architecture")


<img src="overview.PNG?raw=true" />

###### Note:
I **don't** implement the Unit of work and Repository pattern, because (IMO) it's **not** a good idea!
I used this pattern over some projects, but didn't find it useful. It oppositely make me write more dupplicated code. For me, DbContext is already your UnitOfWork, and DbSet will be your Repository.
Visit some articles to read more:
[Why Entity Framework renders the Repository pattern obsolete?](https://cockneycoder.wordpress.com/2013/04/07/why-entity-framework-renders-the-repository-pattern-obsolete/ "Why Entity Framework renders the Repository pattern obsolete?")
[NOT using repository pattern, use the ORM as is (EF)](https://stackoverflow.com/questions/14110890/not-using-repository-pattern-use-the-orm-as-is-ef "NOT using repository pattern, use the ORM as is (EF)")
[Is the Repository pattern useful with Entity Framework? (Series)](https://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework/ "Is the Repository pattern useful with Entity Framework? (Series)")

I think UoW and Repository pattern will be useful in other architectures (such as DDD) or some situations (such as the posibility of changing ORM tool from EF to another).
Again, it's just my opinion, and I **do** know the benefits of UoW and Repository pattern (in theory), but I haven't never got that benefits in reality, hope that I will get it in future projects!