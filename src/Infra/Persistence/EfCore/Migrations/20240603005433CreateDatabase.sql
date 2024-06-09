CREATE DATABASE solidariza_brasil_dev;

CREATE TYPE donation_intent_status AS ENUM ('pending', 'completed');

CREATE TABLE IF NOT EXISTS donation_intents
(
    code                 TEXT      NOT NULL PRIMARY KEY,
    name                 TEXT,
    email                TEXT,
    cpf                  TEXT,
    amount               DECIMAL(10, 2),
    status               DONATION_INTENT_STATUS,
    pending_at           TIMESTAMP,
    completed_at         TIMESTAMP,

    donation_date        DATE,
    donation_amount      DECIMAL(10, 2),
    donation_recorded_at TIMESTAMP,

    created_at           TIMESTAMP NOT NULL,
    updated_at           TIMESTAMP NOT NULL
);

CREATE TYPE financial_transaction_type AS ENUM ('donation', 'withdrawal');

CREATE TABLE IF NOT EXISTS financial_transactions
(
    code                 TEXT      NOT NULL PRIMARY KEY,
    amount               DECIMAL(10, 2),
    transaction_date     TIMESTAMP,
    type                 FINANCIAL_TRANSACTION_TYPE,
    receipt              TEXT,
    
    created_at           TIMESTAMP NOT NULL
);
