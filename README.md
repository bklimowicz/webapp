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

## Endpointy

- `GET /items` — lista rzeczy w domu.
- `POST /items` — dodanie nowej rzeczy, body JSON:

```json
{
  "name": "Herbata",
  "location": "Szafka",
  "quantity": 2
}
```

`quantity` jest opcjonalne i domyślnie przyjmie wartość `1`.
