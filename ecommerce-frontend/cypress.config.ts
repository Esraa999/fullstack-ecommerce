    import { defineConfig } from 'cypress';

    export default defineConfig({
      e2e: {
        baseUrl: 'http://localhost:4200',  // Matches ng serve
        specPattern: 'cypress/e2e/**/*.cy.{js,jsx,ts,tsx}',  // Supports TS
        supportFile: 'cypress/support/e2e.ts',
        fixturesFolder: 'cypress/fixtures',
        screenshotsFolder: 'cypress/screenshots',
        videosFolder: 'cypress/videos',
        video: true,
        viewportWidth: 1280,
        viewportHeight: 720,
        chromeWebSecurity: false,  // Allows API calls to backend (localhost:8080)
        setupNodeEvents(on, config) {
          // Optional: Add plugins (e.g., for Angular)
          return config;
        },
      },
    });
    