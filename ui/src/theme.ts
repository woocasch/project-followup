import { localStorageService, type LocalStorageService } from './infrastructure/local-storage.service';

export const theme = {
    colors: {
        primary: 'var(--colors-primary)',
        secondary: 'var(--colors-secondary)',
        success: 'var(--colors-success)',
        warning: 'var(--colors-warning)',
        error: 'var(--colors-error)',
        background: 'var(--colors-background)',
        surface: 'var(--colors-surface)',
        textPrimary: 'var(--colors-text-primary)',
        textSecondary: 'var(--colors-text-secondary)',
    },
    spaces: {
        xsmall: 'var(--spaces-xsmall)',
        small: 'var(--spaces-small)',
        medium: 'var(--spaces-medium)',
        large: 'var(--spaces-large)',
        xlarge: 'var(--spaces-xlarge)',
    },
    borderRadius: {
        xsmall: 'var(--borderRadius-xsmall)',
        small: 'var(--borderRadius-small)',
        medium: 'var(--borderRadius-medium)',
        large: 'var(--borderRadius-large)',
        xlarge: 'var(--borderRadius-xlarge)',
    }
}

import { useState, useEffect } from 'react';

export enum Theme {
    Light = "light",
    Dark = "dark"
}

export const availableThemes: Theme[] = [Theme.Light, Theme.Dark];

export const useTheme = () => {
    const storage: LocalStorageService = localStorageService;
    const ThemeStorageKey = 'APP:SETTINGS:THEME';
    const [theme, setThemeState] = useState<Theme>(() => {
        if (document.body.dataset.theme) {
            return <Theme>document.body.dataset.theme;
        }

        const storedTheme = storage.getItem<Theme>(ThemeStorageKey);
        return storedTheme || Theme.Light;
    });

    useEffect(() => {
        document.body.dataset.theme = theme;
        storage.setItem<Theme>(ThemeStorageKey, theme);
    }, [theme]);

    const setTheme = (name: Theme) => {
        if (availableThemes.includes(name)) {
            setThemeState(name);
        }
    };

    const nextTheme = () => {
        const index = (availableThemes.indexOf(<Theme>theme) + 1) % availableThemes.length;
        setThemeState(availableThemes[index]);
    };

    return { theme, setTheme, nextTheme };
};
