# Storage API (Key–Value)

## Cíl úlohy
Cílem je vytvořit **triviální HTTP API** (server) a v jiné solution / procesu k němu napsat **klienta**, který API používá.

Budete mít **dva běžící procesy**:
- **Server**: ASP.NET Core Web API (minimal API je povoleno)
- **Client**: Console aplikace, která server volá přes HTTP

Úloha je zaměřená na:
- oddělení serveru a klienta (hranice procesu)
- návrh jednoduchého HTTP kontraktu
- práci se status codes (200 / 201 / 204 / 400 / 404)
- použití `HttpClient` na straně klienta


## Zadání

Naprogramujte jednoduché **key–value storage API**.

Storage ukládá dvojice:
- `key` – identifikátor (string)
- `value` – hodnota (string)

Ukládání může být **in-memory** (bez databáze).


## API kontrakt

### 1) Uložit hodnotu (tzv. upsert)
**PUT** `/kv/{key}`

Body (JSON):
```json
{ "value": "..." }
```

Chování:
- pokud `key` **neexistoval** → `201 Created`
- pokud `key` **existoval a byl přepsán** → `204 No Content`
- nevalidní `key` nebo `value`, chybějící body → `400 Bad Request`


### 2) Načíst hodnotu
**GET** `/kv/{key}`

Chování:
- pokud `key` existuje → `200 OK` + body:
```json
{ "key": "foo", "value": "bar" }
```
- pokud `key` neexistuje → `404 Not Found`


### 3) Smazat klíč
**DELETE** `/kv/{key}`

Chování:
- pokud `key` existuje → `204 No Content`
- pokud `key` neexistuje → `404 Not Found`


### 4) Vypsat klíče (dobrovolné)
**GET** `/kv?prefix=abc`

Chování:
- vrátí `200 OK` + body:
```json
{ "keys": ["abc:one", "abc:two"] }
```

Pokud `prefix` není zadán, vraťte všechny klíče.


## Validace

### Key
- délka **1–50**
- povolené znaky: `a-z A-Z 0-9 : _ -`

### Value
- délka **0–2000**

Při porušení pravidel vraťte `400 Bad Request`.

## Klient (Console aplikace)

Podporované příkazy:
- `set <key> <value>`
- `get <key>`
- `del <key>`
- `list [prefix]`


## Technické požadavky

- Server běží na `localhost`
- Řešení je ve **dvou projektech** (server + client)
- Storage je thread-safe (`ConcurrentDictionary`)


## Dobrovolné bonusy

- TTL
- Statistiky
- Swagger / OpenAPI
- Batch endpoint
