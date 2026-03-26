# ReqRes Refit Demo (Console + DI)

Vzorové řešení: Refit klient pro ReqRes (login + users).

- na startu se aplikace zeptá na `x-api-key` (z https://app.reqres.in)
- provede `POST /api/login` (demo credentials)
- nastaví `Authorization: Bearer <token>` pro další requesty
- ukázkově zavolá:
  - `GET /api/users?page=1`
  - `GET /api/users/2`
  - `POST /api/users`
  - `PUT /api/users/2`
  - `DELETE /api/users/2`

Specifikace API: https://reqres.in/openapi.json
