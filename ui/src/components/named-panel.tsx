import styled from '@emotion/styled';
import { theme } from '@root/theme';
import type { ReactNode } from 'react';

interface NamedPanelContentProps {
  children: ReactNode;
}

interface NamedPanelProps {
  title: string;
  children: ReactNode;
}

const NamedPanelStyled = styled.div(`
    isolation: isolate;
    h2 {
        margin-bottom: 0;
        border: solid black 1px;
        border-bottom: none;
        border-radius: ${theme.borderRadius.medium};
        border-bottom-left-radius: 0;
        border-bottom-right-radius: 0;
        width: fit-content;
        padding: 0.5em;
        font-weight: bold;
    }
`);

const NamedPanelContentStyled = styled.div(`
    border: solid black 1px;
    border-radius: ${theme.borderRadius.medium};
    border-top-left-radius: 0;
    padding: ${theme.spaces.medium};
`);

function NamedPanelContent({ children }: NamedPanelContentProps) {
  return <NamedPanelContentStyled>{children}</NamedPanelContentStyled>;
}

function NamedPanelRoot(props: NamedPanelProps) {
  const { children, title } = props;
  return (
    <NamedPanelStyled>
      <h2>{title}</h2>
      <NamedPanelContent>{children}</NamedPanelContent>
    </NamedPanelStyled>
  );
}

export const NamedPanel = Object.assign(NamedPanelRoot, {
  Content: NamedPanelContent,
});
