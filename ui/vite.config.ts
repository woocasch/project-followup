import react from '@vitejs/plugin-react';
import { defineConfig, type UserConfig } from 'vite';
import tsconfigPaths from 'vite-tsconfig-paths';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react(), tsconfigPaths()],
  test: {
    environment: 'jsdom',
    // hey! 👋 over here
    globals: true,
    setupFiles: './vite.setup.js',
  },
} as UserConfig);
