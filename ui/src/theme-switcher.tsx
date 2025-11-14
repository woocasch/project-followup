import { Moon, Sun } from 'lucide-react';
import { availableThemes, Theme, useTheme } from './theme';

interface ThemeRepresentationProps {
  currentTheme: Theme;
  onClick: () => void;
}

function ThemeRepresentation(props: ThemeRepresentationProps) {
  const { currentTheme: theme, onClick } = props;

  if (availableThemes.length === 1) {
    return null;
  }

  return (
    <>
      {theme === Theme.Light ? <Sun onClick={onClick} /> : null}
      {theme === Theme.Dark ? <Moon onClick={onClick} /> : null}
    </>
  );
}

export default function ThemeSwitcher() {
  const themeOperator = useTheme();
  const onClick = () => {
    themeOperator.nextTheme();
  };

  return (
    <ThemeRepresentation currentTheme={themeOperator.theme} onClick={onClick} />
  );
}
