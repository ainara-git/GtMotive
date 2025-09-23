# Microservicio de Alquiler de Vehículos - GT Motive

Sistema de gestión de alquiler de vehículos implementado a modo de ejemplo para Clean Architecture, Domain-Driven Design (DDD) y patrones CQRS.

## Asunciones y simplificaciones
Se asume que el valor del ejercicio esta en desarrollar software aplicando los patrones mencionados en el readme original del ejercicio es por eso que se hacen las siguientes simplificaciones: 

| Decisión                                     | Pros                                                                        | Cons                                                        |
|----------------------------------------------|-----------------------------------------------------------------------------|-------------------------------------------------------------|
| MongoDB Standalone es suficiente             | Simplicidad en configuración y desarrollo rapido                            | Sin transacciones ACID, limitando garantías de consistencia |
| RabbitMQ básico es suficiente                | Configuración simple, mínima infraestructura                                | Sin garantías de entrega                                    |
| No es necesario Outbox Pattern               | Limitar el alcance del ejercicio                                            | Posible pérdida de eventos de dominio en fallos             |


## Flujo: Use Case + Arquitectura Hexagonal + CQRS
```
HTTP Request  → Controller → Command / Query → Handler → UseCase ┬→ Domain
                     ↓                                     |     ├→ Repository → Database
                  Presenter   ←   OutputPort  ←  Result   ←┘     ├→ UnitOfWork → Transaction
                     ↓                                           └→ EventBus → Message Queue
                ActionResult
```
## Domain Driven Design

### Reglas de Negocio e Invariantes

- **Edad Máxima de Vehículos en Flota: 5 años**
  - `Vehicle`
  - `VehiclesCollection.FilterOutOldVehicles()`
- **1 Cliente Máximo 1 Alquiler Activo**
  - `RentalsCollection.EnsureCustomerCanRent()`


## Endpoints de la API

### Vehicles Controller
- `POST /api/vehicles` - Agregar vehículo
- `POST /api/vehicles/available` - Consultar vehículos disponibles
- `POST /api/vehicles/rent` - Alquilar vehículo
- `PUT /api/vehicles/return` - Devolver vehículo

## Qué Hacer con Más Tiempo
- **Transacciones reales en MongoDB** Habilitar replica set para soporte de transacciones ACID e implementar `IUnitOfWork` completo en repositories.
- **Azure Service Bus** en lugar de **RabbitMQ** 
- **Outbox Pattern** Garantizar consistencia entre persistencia y eventos
- **Revisar concurrencias**
- **Healthchecks**
- **Mejorar el manejo de errores y logging**
- **Mejorar tests**

## Cómo Construir y Ejecutar

### Prerrequisitos
- .NET 9.0 SDK
- Docker y Docker Compose
- MongoDB (proporcionado vía Docker)
- RabbitMQ (proporcionado vía Docker)
  
### Ejecutar en el directorio de docker-compose.yml

```bash
# Compilar la api
docker compose build --no-cache

# Levantar todos los servicios
docker-compose up -d

# La API estará disponible en: http://localhost:5000
# Swagger UI de la API en: http://localhost:5000/swagger/index.html
# UI MongoDB en http://localhost:8081 (admin| password)
# UI RabbitMQ en http://localhost:15672 (admin| password)

# Parar todos los servicios
docker-compose down -v
```
