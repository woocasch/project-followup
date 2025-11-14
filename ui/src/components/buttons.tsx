import styled from '@emotion/styled';
import { theme } from '@root/theme';
import type { LucideIcon } from 'lucide-react';
import type { ReactNode } from 'react';

export type ButtonVariant = 'button' | 'success' | 'warning' | 'error';

export interface ButtonProps {
  children: ReactNode;
  variant?: ButtonVariant;
  title?: string;
  onClick?: () => void;
}

export interface IconButtonProps {
  icon: LucideIcon;
  onClick?: () => void;
}

function getBackgroundColor(variant?: ButtonVariant) {
  switch (variant) {
    case 'success':
      return theme.colors.success;
    case 'warning':
      return theme.colors.warning;
    case 'error':
      return theme.colors.error;
    default:
      return theme.colors.background;
  }
}

function getFontColor(variant?: ButtonVariant) {
  switch (variant) {
    case 'success':
      return theme.colors.successText;
    case 'warning':
      return theme.colors.warningText;
    case 'error':
      return theme.colors.errorText;
    default:
      return theme.colors.textPrimary;
  }
}

const ButtonStyled = styled.button<{ variant?: ButtonVariant }>`
    background-color: ${(props) => getBackgroundColor(props.variant)};
    color: ${(props) => getFontColor(props.variant)};
    padding: ${theme.spaces.xsmall};
`;

export function Button(props: ButtonProps) {
  const {
    children,
    variant = 'button',
    title = '',
    onClick = () => {},
  } = props;

  return (
    <ButtonStyled variant={variant} onClick={onClick} title={title}>
      {children}
    </ButtonStyled>
  );
}

export function IconButton(props: IconButtonProps) {
  const { icon: Icon, onClick = () => {} } = props;
  const StyledIcon = styled(Icon)`
        display: inline`;
  return <StyledIcon onClick={onClick} />;
}
