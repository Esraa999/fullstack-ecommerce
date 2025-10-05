/// <reference types="cypress" />

// Custom command: Login with username/password
Cypress.Commands.add('login', (username: string, password: string) => {
  cy.visit('/login');  // Assume /login route
  cy.get('input[formControlName="username"]').type(username);  // Adjust selector
  cy.get('input[formControlName="password"]').type(password);
  cy.get('button[type="submit"]').click();
  cy.url().should('include', '/dashboard');  // Redirect after login
});

// Mock API response (for offline testing)
Cypress.Commands.add('mockLogin', () => {
  cy.intercept('POST', '/api/auth/login', {
    statusCode: 200,
    body: { token: 'mock-jwt', user: { id: 1, username: 'test' } }
  }).as('login');
});