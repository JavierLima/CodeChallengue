# CodeChallenge

Api capaz de gestionar perros. Consta de una aplicación REST con peticiones HTTP tales como GET, POST, DELETE y PUT. Además el código autogenera un swagger para probar y documentar el sistema.

### Pre-requisitos 📋

* **Git** - Para poder instalar git hay que decargarse la herramienta [Git](https://git-scm.com/downloads)

En sistemas Unix se puede instalar con el siguiente comando
```
sudo apt install git
```
* **.NET Core** - Para instalar .NET hay que descargárselo de la página web de [Microsoft](https://docs.microsoft.com/es-es/aspnet/core/getting-started/?view=aspnetcore-3.1&tabs=windows).
* **Visual Studio** - En este caso se ha utilizado el IDE [VisualStudio](https://visualstudio.microsoft.com/es/vs/).
Asegurarse de que en Visual Studio tenemos instalado la carga de trabajo desarrollo de ASP. NET Core:
[
![image](https://media.discordapp.net/attachments/362157432534990848/809512096915652638/unknown.png?width=1200&height=670)
](url)

* **Postman** - Para el control de pruebas [Postman](https://www.postman.com/downloads/).


### Instalación 🔧

Vamos a clonar el repositorio y abrirlo en Visual Studio. 

Para instalar la aplicación desde GitHub hay que abrir una terminal de comandos y clonar el repositorio. Para clonarlo hay que ejecutar el siguiente comando:

```
git clone https://github.com/JavierLima/CodeChallengue.git
```
Una vez clonado el repositorio procedemos a abrirlo con nuestro IDE de desarrollo.

Para abrir el proyecto hay que acceder al IDE y una vez dentro saldrá la opción de cargar un proyecto desde una carpeta local.

[
![image](https://user-images.githubusercontent.com/40237259/107687677-143fdd00-6ca7-11eb-9957-9cd0ca89d6e0.png)
](url)

Buscamos el archivo CodeChallenge.sln, lo seleccionamos y se debería abrir el proyecto en Visual Studio.

## Despliegue 📦

Para ejecutar el programa en una máquina local debemos clicar el siguiente play verde:

![image](https://user-images.githubusercontent.com/40237259/107688868-6b927d00-6ca8-11eb-8431-bddea05b30e1.png)

Una vez que cliquemos el play podremos saber si todo ha ido bien debería aparecer la siguiente página web "http://localhost:58057/swagger/index.html" con la documentación de swagger.

## Ejecutando las pruebas ⚙️

_Por un lado tenemos las pruebas unitarias realizadas con [NUnit](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-nunit) y una colección realizada en [Postman](https://www.postman.com/)._

### Analizando las pruebas NUnit 🔩

Las pruebas unitarias se han hecho con el propósito de encontrar defectos en la aplicación. Durante el período de prueba, el código implementado para la gestión de la API ha sufrido cambios.

Para llevar a cabo dichas pruebas deberíamos seleccionar el proyecto llamado NUnitTestCodeChallenge con clic derecho y seleccionar ejecutar pruebas:

![image](https://user-images.githubusercontent.com/40237259/107689332-fbd0c200-6ca8-11eb-94c6-f8afa42f0e02.png)

### Analizando las pruebas Postman ⌨️

Las pruebas con Postman se han hecho con el fin de probar el despliegue de las peticiones web desde una herramienta fiable. 

A través de una colección se han añadido distintas pruebas escritas en JavaScript que verifican el correcto funcionamiento de la API.

Para poder probarlo necesitamos ejecutar Postman y darle clic a Import con la funcionalidad de File, deberías o seleccionar el archivo o arrastralo a Postman.

![image](https://cdn.discordapp.com/attachments/362157432534990848/809514858898784266/unknown.png)

Una vez cargada la colección procedamos a iniciar las pruebas. Antes que nada asegurarse de que la aplicación está corriendo en Visual Studio para que no haya errores.

Para ejecutar las pruebas seleccionamos la colección y presionamos el play, debería salir una pestaña donde nos permita ejecutar Run y probar los tests.

![image](https://cdn.discordapp.com/attachments/362157432534990848/809515922259312700/unknown.png)

## Construido con 🛠️

* [C#](https://docs.microsoft.com/es-es/dotnet/csharp/) - Lenguaje usado
* [VisualStudio](https://visualstudio.microsoft.com/es/vs/) - El IDE usado para el desarrollo
* [ASP.NET Core](https://docs.microsoft.com/es-es/aspnet/core/?view=aspnetcore-5.0) - Proyecto web
* [Postman](https://www.postman.com/) - Usado para elaborar pruebas
* [NUnit](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-nunit) - Usado para elaborar pruebas unitarias


## Autor ✒️

* **Javier Lima** [JavierLima](https://github.com/JavierLima)
