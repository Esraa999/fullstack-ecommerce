describe('E-Commerce Frontend E2E', () => {
  beforeEach(() => {
    cy.mockLogin();  // Mock API if backend down
  });

  it('should load the app and display root component', () => {
    cy.visit('/');  // ng serve URL
    cy.title().should('eq', 'E-Commerce App');  // Adjust to your <title> in index.html
    cy.get('app-root').should('be.visible');  // Angular root
    cy.contains('Welcome to E-Commerce').should('exist');  // Skeleton text?
  });

  it('should handle login flow', () => {
    cy.visit('/login');
    cy.get('h1').should('contain', 'Login');  // Page header
    cy.login('testuser', 'password123');  // Uses custom command
    cy.contains('Dashboard').should('be.visible');  // Success redirect
  });

  it('should navigate to products (protected route)', () => {
    cy.login('testuser', 'password123');
    cy.visit('/products');  // Assume route
    cy.get('app-product-list').should('exist');  // Component check
  });
});