# Firebase Authentication for ASP.NET Core

### Installation

`Install-Package Ilan321.FirebaseAuthentication`

### Setup and Usage

1. If you haven't already, register an app on the [Firebase Console](https://console.firebase.google.com "Firebase Console").
2. Follow the instructions on the [Auth Admin SDK](https://firebase.google.com/docs/admin/setup/ "Auth Admin SDK") page to setup the Firebase App instance.
3. Import the ``Ilan321.FirebaseAuthentication`` namespace and call ``UseFirebaseAuthentication()`` in ``ConfigureServices()``.

### Example

```csharp
using Ilan321.FirebaseAuthentication;

// ...

public void ConfigureServices(IServiceCollection services)
{
	services
		.AddAuthentication(FirebaseAuthenticationDefaults.SchemeName)
		.AddFirebaseAuthentication();

	// ...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
	// ...

	app.UseAuthentication();
	app.UseAuthorization();

	// ...
}
```

