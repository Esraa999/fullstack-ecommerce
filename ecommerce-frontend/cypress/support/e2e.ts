// Cypress support file for E2E tests
import './commands';  // Custom commands (create commands.ts if needed)

// Before each test: Reset state
beforeEach(() => {
  cy.clearCookies();  // Clean auth state
  cy.visit('/');  // Start at home (your app root)
});

// Global styles or assertions
Cypress.on('uncaught:exception', (err, runnable) => {
  // Ignore Angular errors (e.g., zone.js)
  return false;
});