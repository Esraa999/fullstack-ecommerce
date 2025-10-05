// src/test.ts

import 'zone.js/testing';
import { getTestBed } from '@angular/core/testing';
import {
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting,
} from '@angular/platform-browser-dynamic/testing';

getTestBed().initTestEnvironment(
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting(),
);

// ✅ Explicit static imports (no async)
import './app/services/auth.service.spec';
// Add more here:
// import './app/app.component.spec';
// import './app/guards/auth.guard.spec';
