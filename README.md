
# 💰 GastosControl

**GastosControl** es una aplicación web construida en .NET 8 con arquitectura limpia, diseñada para registrar, gestionar y visualizar gastos personales por usuario. Incluye autenticación, presupuesto mensual, tipos de gasto, depósitos, validaciones de sobregiro y un dashboard visual.

---

## 📦 Tecnologías utilizadas

- ASP.NET Core 8 (WebApp MVC)
- SQL Server
- Entity Framework Core
- Docker
- Railway (Deploy)
- Bootstrap 5

---

## 📁 Estructura del Proyecto

```
📦 GastosControlApp/
├── GastosControl/                # Proyecto principal ASP.NET Core (UI y API)
├── GastosControl.Application/   # Casos de uso, servicios y DTOs
├── GastosControl.Domain/        # Entidades y reglas de negocio
├── GastosControl.Infrastructure/ # Persistencia y repositorios
└── Dockerfile                   # Imagen para Railway
```

---

## 🚀 Cómo ejecutar en local

1. Clonar el repositorio:

```bash
git clone https://github.com/GilberMena/GastosControl.git
cd GastosControl/GastosControl
```

2. Configurar cadena de conexión (`appsettings.json`):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=GastosControlDb;User Id=sa;Password=yourStrong(!)Password;"
  }
}
```

3. Aplicar migraciones (si es necesario):

```bash
Update-Database -StartupProject GastosControl -Project GastosControl.Infrastructure
```

4. Ejecutar la aplicación:

```bash
dotnet run
```

Acceder en `http://localhost:5169` o el puerto que se indique.

---

## 📊 Funcionalidades clave

- ✅ Autenticación de usuarios
- ✅ CRUD de tipos de gasto
- ✅ Presupuestos por mes y tipo
- ✅ Registro de gastos con validación de sobregiro
- ✅ Depósitos a fondo monetario
- ✅ Dashboard con resumen visual (Top gastos, ejecución vs presupuesto)

---

## 🧪 Capturas

| Dashboard principal | Registro de gasto | Gráfico presupuestal |
|---------------------|-------------------|-----------------------|
| ![dashboard](![image](https://github.com/user-attachments/assets/712b2cbd-210e-4712-bbf6-38ed089cca9e)| ![registro](![image](https://github.com/user-attachments/assets/f4b30b46-adcd-42a0-8336-7f772d169ab1)| ![grafico](![image](https://github.com/user-attachments/assets/87f0fd09-a17b-41ce-aba9-10e87cb28293) |

---

## 📄 Licencia

Este proyecto se distribuye bajo la Licencia MIT. Consulta el archivo `LICENSE`.

---

## ✍️ Autor

Desarrollado por **Gilber Mena** - [GitHub](https://github.com/GilberMena)  
Proyecto de prueba técnica para portafolio y despliegue educativo.
