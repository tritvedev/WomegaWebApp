# WomegaWebApp

This is an E-Commerce Web Application for Womega Online Shopping.

## Requirement

- .NET Core
- vuejs
- Bulma - CSS framework for UI

## Description

### .NET Core
  - Application Layer - Products/ Stock/ Order
  - Entity Framework Core - Database
  - Authentication - Identity, Claim-Based, Admin, User
  - Sessions/Cache - Cart History

### Admin Control Panel
  - Creating / Managing Products
  - Managing Stocks
  - Managing Orders
  - Vuejs + Bulma -> Both for Ui, Vuejs specifically for Admin Control Panel. Most of UI is done is Bulma.

### WomegaWebApp Shop

  - Product - Category/ Type filter
  - Cart
  - Checkout
  - Payment - stripe.js

## Layers (internal Projects)

In Application layer, all it's going to do is handle all of our models in our domain between the database  & UI layer. Thus, Application acts like API for the WomegaWebApp.
