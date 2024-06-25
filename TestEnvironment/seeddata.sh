curl -X 'POST' \
  'http://localhost:5001/api/v1/User' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "Henrik42",
  "password": "1234",
  "email": "hnrkjnsn@email.io",
  "registrationDate": "2024-05-31T12:23:04.448Z",
  "isActive": true,
  "fullName": "Henrik Jensen",
  "address": "NoStreet 332, Alibuma",
  "phoneNumber": "123456789"
}'

curl -X 'POST' \
  'http://localhost:5001/api/v1/User' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "Johan12",
  "password": "4321",
  "email": "john@lololo.io",
  "registrationDate": "2024-05-31T12:23:04.448Z",
  "isActive": true,
  "fullName": "Johan Back",
  "address": "Everywhere 3821, Neverland",
  "phoneNumber": "8728881299"
}'