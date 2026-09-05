docker run \
  --name blackjack-postgres \
  -e POSTGRES_PASSWORD='Password123$' \
  -e POSTGRES_DB=blackjack \
  -v pgdata_blackjack:/var/lib/postgresql \
  -p 5432:5432 \
  postgres:18
