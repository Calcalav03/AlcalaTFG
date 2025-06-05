PesKApp PesKApp es una aplicación de escritorio multiplataforma desarrollada en .NET MAUI que permite a los aficionados a la pesca deportiva registrar, gestionar y compartir sus capturas. El backend está implementado en Spring Boot con Hibernate y expone servicios REST sobre una base de datos PostgreSQL.

📌 Características principales Autenticación y registro de usuarios

Creación de cuenta con verificación de correo y código de activación.

Inicio de sesión seguro con JSON Web Token (JWT).

Gestión de capturas

Añadir, editar y eliminar capturas (especie, peso, tamaño, ubicación, fecha e imagen en Base64).

Asociar cebos, equipamiento, clima y métodos de pesca a cada captura.

Visualizar un listado global (“red social”) y un listado privado de capturas propias.

Catálogos auxiliares

CRUD de Cebos: tipo y descripción.

CRUD de Equipamiento: tipo, marca y modelo.

Interfaz de usuario en MAUI

Patrones MVVM con RelayCommand para comandos asíncronos.

CollectionView con diseño de cuadrícula para mostrar tarjetas de capturas.

Popups (Mopup) para visualizar detalles, editar o eliminar capturas desde la misma vista.

Backend con Spring Boot + Hibernate

Entidades JPA para Captura, Usuario, Cebo, Equipamiento, Clima y MétodosPesca.

Repositorios Spring Data JPA con proyecciones para respuestas parciales (solo campos necesarios).

Endpoints REST para todas las operaciones CRUD, incluyendo manejo de relaciones muchos-a-muchos.

🔧 Tecnologías Frontend (.NET MAUI)

Lenguaje: C#

MVVM (CommunityToolkit.Mvvm)

FFImageLoading.Maui (carga de imágenes Base64)

Mopups (popups modales para detalles)

SecureStorage (almacenamiento seguro de token y usuario)

Backend (Spring Boot)

Lenguaje: Java

Spring Boot (servicios REST, seguridad básica)

Spring Data JPA + Hibernate (mapeo de entidades a PostgreSQL)

PostgreSQL (base de datos relacional)

Herramientas auxiliares

IntelliJ IDEA (desarrollo backend)

PyCharm (scripts Flask para envío de correo y microservicios opcionales)

Git (control de versiones)

GitHub (repositorio remoto)

📋 Requisitos previos Backend

Java 11 o superior

Maven o Gradle

PostgreSQL (creación de base de datos y usuario)

Frontend

.NET 8 SDK

Visual Studio 2022+ con carga de trabajo “NET MAUI”

Windows 10/11 o macOS (para compilación MAUI multiplataforma)

🚀 Instalación y puesta en marcha Clonar repositorio

bash Copiar Editar git clone https://github.com/tu-usuario/PesKApp.git cd PesKApp Configuración de la base de datos PostgreSQL

Crear una base de datos llamada peskapp.

Ejecutar el script SQL de creación de tablas y secuencias (incluido en backend/src/main/resources/schema.sql).

Ajustar credenciales en backend/src/main/resources/application.properties:

ini Copiar Editar spring.datasource.url=jdbc:postgresql://localhost:5432/peskapp spring.datasource.username=<tu_usuario> spring.datasource.password=<tu_contraseña> Arrancar el backend

bash Copiar Editar cd PesKApp/backend mvn spring-boot:run El servidor REST quedará disponible en http://localhost:8089/jpa/.

Arrancar la aplicación MAUI

Abrir la solución .sln en Visual Studio.

Seleccionar “Debug | PesKApp.MAUI” como proyecto de inicio.

Ejecutar en “Windows Machine” (o Android/iOS si está configurado).

🗂 Estructura de carpetas bash Copiar Editar PesKApp/ │ ├─ backend/ # Proyecto Spring Boot │ ├─ src/main/java/… │ ├─ src/main/resources/ │ │ ├─ application.properties │ │ └─ schema.sql │ └─ pom.xml │ ├─ PesKApp.MAUI/ # Proyecto .NET MAUI (cliente) │ ├─ Converters/ │ ├─ Models/ │ ├─ Services/ │ ├─ ViewModels/ │ ├─ Views/ │ ├─ Resources/ │ └─ PesKApp.MAUI.csproj │ └─ README.md 📐 Diagramas y casos de uso Modelo entidad-relación (ERD)

Usuarios (1) – (Capturas) (N)

Capturas (1) – (Clima) (N)

Capturas (1) – (MétodosPesca) (N)

Capturas (N) – (Cebos) (N) (vía tabla intermedia capturas_cebos)

Capturas (N) – (Equipamiento) (N) (vía tabla intermedia capturas_equipamiento)

Principales casos de uso

Registro de usuario

Login/Logout

Crear/editar/eliminar captura

Listar capturas globales y propias

Gestionar cebos y equipamiento

Diagramas de secuencia

Inicio de sesión: JSON (WebView MAUI) → Spring Boot → PostgreSQL → respuesta JWT.

Alta de captura: Cliente MAUI envía datos JSON → Servicio REST → Repositorio JPA guarda en varias tablas.

📝 Uso Registro y activación:

Abrir MAUI → “Registro” → ingresar nombre, correo y contraseña.

Recibirás un código de activación por correo (servidor Flask integrado).

Ingresar el código en “Activar cuenta”.

Login:

Ingresar correo/usuario y contraseña.

Guardar token JWT en SecureStorage.

Menú principal:

Nueva captura: formulario con campos (especie, peso, tamaño, ubicación, fecha, imagen Base64, seleccionar cebos, equipamiento, clima y método).

Catálogo de cebos y equipamiento: agregar/editar/eliminar registros.

Ver todas las capturas: listado en cuadrícula. Doble click abre popup con detalles (editar/borrar).

Ver mis capturas: equivalente anterior, filtrado por usuario logueado.

Detalles y edición rápida:

En el listado, haz doble click sobre la tarjeta de captura para abrir popup con todos los datos y opciones de editar o borrar.

Confirmaciones mediante DisplayAlert.

🔒 Seguridad Las contraseñas se almacenan en PostgreSQL usando crypt (bcrypt).

Se verifica estado = true tras activar usuario.

➕ Contribuciones Hacer un fork de este repositorio.

Crear una rama con tu nueva funcionalidad (git checkout -b feature/lo-que-hagas).

Realizar cambios, pruebas y commits (git commit -m "Agrega …").

Subir a tu fork (git push origin feature/lo-que-hagas).

Abrir un Pull Request describiendo tu propuesta.

📝 Licencia Este proyecto está bajo la licencia MIT. Consulta el archivo LICENSE para más detalles.

¡Gracias por usar PesKApp! Esperamos que disfrutes registrando y compartiendo tus mejores capturas.
