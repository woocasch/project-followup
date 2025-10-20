export const theme = {
    colors: {
        primary: 'var(--colors-primary)',
        background: 'var(--colors-background)',
        panelheader: 'var(--colors-panelheader)',
    },
}

import { useState, useEffect } from 'react';

export enum Theme {
    Light = "light",
    Dark = "dark"
}

export const availableThemes: Theme[] = [Theme.Light];//, Theme.Dark];

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
