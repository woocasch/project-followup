export const theme = {
    colors: {
        primary: {
            basic: 'var(--color-primary)',
            light: 'var(--color-primary-light)',
            dark: 'var(--color-primary-dark)',
            contrast: 'var(--color-primary-contrast)',
        },
        secondary: {
            basic: 'var(--color-secondary)',
            light: 'var(--color-secondary-light)',
            dark: 'var(--color-secondary-dark)',
            contrast: 'var(--color-secondary-contrast)',
        },
        accent: {
            basic: 'var(--color-accent)',
            light: 'var(--color-accent-light)',
            dark: 'var(--color-accent-dark)',
        },
        background:{
            primary: 'var(--color-bg-primary)',
            secondary: 'var(--color-bg-secondary)',
            tertiary: 'var(--color-bg-tertiary)',
            accent: 'var(--color-bg-accent)',
        },
        surface: {
            basic: 'var(--color-surface)',
            raised: 'var(--color-surface-raised)',
            overlay: 'var(--color-surface-overlay)',
        },
        text: {
            primary: 'var(--color-text-primary)',
            secondary: 'var(--color-text-secondary)',
            tertiary: 'var(--color-text-tertiary)',
            inverse: 'var(--color-text-inverse)',
            success: 'var(--color-text-success)',
            warning: 'var(--color-text-warning)',
            error: 'var(--color-text-error)',
            info: 'var(--color-text-info)',
        },
        border: {
            primary: 'var(--color-border-primary)',
            secondary: 'var(--color-border-secondary)',
            accent: 'var(--color-border-accent)',
            success: 'var(--color-border-success)',
            warning: 'var(--color-border-warning)',
            error: 'var(--color-border-error)',
            info: 'var(--color-border-info)',
        },
        interactiveStates: {
            hover: 'var(--color-interactive-hover)',
            active: 'var(--color-interactive-active)',
            focus: 'var(--color-interactive-focus)',
            disabled: 'var(--color-interactive-disabled)',
            disabledText: 'var(--color-interactive-disabled-text)',
        },
        status: {
            success: 'var(--color-success)',
            successLight: 'var(--color-success-light)',
            successDark: 'var(--color-success-dark)',
            warning: 'var(--color-warning)',
            warningLight: 'var(--color-warning-light)',
            warningDark: 'var(--color-warning-dark)',
            errror: 'var(--color-error)',
            errorLight: 'var(--color-error-light)',
            errorDark: 'var(--color-error-dark)',
            info: 'var(--color-info)',
            infoLight: 'var(--color-info-light)',
            infoDark: 'var(--color-info-dark)',
        },
        shadows: {
            small: 'var(--shadow-sm)',
            normal: 'var(--shadow)',
            medium: 'var(--shadow-md)',
            large: 'var(--shadow-lg)',
            xlarge: 'var(--shadow-xl)',
            green: 'var(--shadow-green)',
        },
        borderRadius: {
            small: 'var(--radius-sm)',
            standard: 'var(--radius)',
            medium: 'var(--radius-md)',
            large: 'var(--radius-lg)',
            xlarge: 'var(--radius-xl)',
            full: 'var(--radius-full)',
        },
        spacing: {
            xsmall: 'var(--spacing-xs)',
            small: 'var(--spacing-sm)',
            standard: 'var(--spacing)',
            medium: 'var(--spacing-md)',
            large: 'var(--spacing-lg)',
            xlarge: 'var(--spacing-xl)',
        },
        typography: {
            xsmall: 'var(--font-size-xs)',
            small: 'var(--font-size-sm)',
            standard: 'var(--font-size)',
            large: 'var(--font-size-lg)',
            xlarge: 'var(--font-size-xl)',
            xxlarge: 'var(--font-size-2xl)',
            xxxlarge: 'var(--font-size-3xl)',
            xxxxlarge: 'var(--font-size-4xl)',
            normal:  'var(--font-weight-normal)',
            medium: 'var(--font-weight-medium)',
            semibold: 'var(--font-weight-semibold)',
            bold: 'var(--font-weight-bold)',
        },
        transitions: {
            fast: 'var(--transition-fast)',
            standard: 'var(--transition)',
            slow: 'var(--transition-slow)',
        },
    },
}

import { useState, useEffect } from 'react';

export enum Theme {
    Light = "light",
    Dark = "dark"
}

export const availableThemes: Theme[] = [Theme.Light, Theme.Dark];

export const useTheme = () => {
    const [theme, setThemeState] = useState<Theme>(() => {
        if (document.body.dataset.theme) {
            return <Theme>document.body.dataset.theme;
        }
        return Theme.Light;
    });

    useEffect(() => {
        document.body.dataset.theme = theme;
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
