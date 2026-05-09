# WebApp boilerplate

## Uruchomienie PostgreSQL w Dockerze

```bash
cd infra
docker compose up -d
```

Domyślna konfiguracja bazy:
- `POSTGRES_DB=webapp`
- `POSTGRES_USER=webapp`
- `POSTGRES_PASSWORD=webapp`
- port `5432`

## Uruchomienie API

```bash
dotnet run --project WebApp.Api
```

Przy starcie API tworzy bazę i seeduje dane startowe.

## Architektura

Projekt stosuje clean architecture z podziałem na warstwy:
- `WebApp.Domain` — encja `HouseholdItem`
- `WebApp.Application` — interfejs repozytorium `IHouseholdItemRepository` oraz kontrakty DTO
- `WebApp.Infrastructure` — implementacja repozytorium, `DbContext`, seed danych
- `WebApp.Api` — endpointy REST w `Endpoints/ItemEndpoints.cs`, `Program.cs` służy wyłącznie do konfiguracji

## Endpointy

- `GET /api/items` — lista wszystkich rzeczy
- `GET /api/items/{id}` — szczegóły pojedynczej rzeczy
- `POST /api/items` — dodanie nowej rzeczy
- `PUT /api/items/{id}` — pełna aktualizacja rzeczy
- `DELETE /api/items/{id}` — usunięcie rzeczy

Przykładowe body dla `POST /api/items` i `PUT /api/items/{id}`:

```json
{
  "name": "Herbata",
  "location": "Szafka",
  "quantity": 2
}
```

`quantity` jest opcjonalne i domyślnie przyjmie wartość `1`.

