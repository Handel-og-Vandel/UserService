# Howto TaxaManager

## Setup environment

export MongoConnectionString=mongodb://admin:1234@localhost:27017/   # ?authMechanism=DEFAULT <-- NO
export DatabaseName=UserDB
export CollectionName=users

## Create a new user with curl

curl -X 'POST' \
  'http://localhost:5240/User' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": {},
  "givenName": "Henrik",
  "familyName": "Jensen",
  "department": "Development",
  "password": "1234",
  "email": "hnrk@qgt.dk",
  "loginIdentifier": "hnrk"
}'
