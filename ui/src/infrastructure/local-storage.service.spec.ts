import { BrowserLocalStorageService } from "./local-storage.service";
import { afterEach, beforeEach, describe, expect, it } from 'vitest';

describe('BrowserLocalStorageService', () => {
    let service: BrowserLocalStorageService;

    beforeEach(() => {
        service = new BrowserLocalStorageService();
    });

    afterEach(() => {
        localStorage.clear();
    });

    it('should store and retrieve an item', () => {
        service.setItem('test', { hello: 'world' });
        const item = service.getItem<{ hello: string }>('test');
        expect(item).toEqual({ hello: 'world' });
    });

    it('should return null for a non-existent item', () => {
        const item = service.getItem('nonExistent');
        expect(item).toBeNull();
    });

    it('should remove an item', () => {
        service.setItem('test', { hello: 'world' });
        service.removeItem('test');
        const item = service.getItem('test');
        expect(item).toBeNull();
    });
});
