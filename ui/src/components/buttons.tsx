import styled from '@emotion/styled';
import { theme } from '@root/theme';
import type { LucideIcon } from 'lucide-react';
import type { ReactNode } from 'react';

export type ButtonVariant =
  | 'button'
  | 'success'
  | 'warning'
  | 'error'
  | 'action';
export type ButtonType = 'standard' | 'rounded';

export interface ButtonProps {
  children: ReactNode;
  variant?: ButtonVariant;
  buttonType?: ButtonType;
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
    case 'action':
      return theme.colors.surface;
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

const ButtonStyled = styled.button<{
  variant?: ButtonVariant;
  buttonType?: ButtonType;
}>`
    background-color: ${(props) => getBackgroundColor(props.variant)};
    color: ${(props) => getFontColor(props.variant)};
    padding: ${theme.spaces.xsmall};
    border-radius: ${(props) => (props.buttonType === 'rounded' ? theme.borderRadius.small : '0')};
`;

export function Button(props: ButtonProps) {
  const {
    children,
    variant = 'button',
    buttonType = 'standard',
    title = '',
    onClick = () => {},
  } = props;

  return (
    <ButtonStyled
      variant={variant}
      buttonType={buttonType}
      onClick={onClick}
      title={title}
    >
      {children}
    </ButtonStyled>
  );
}

export function IconButton(props: IconButtonProps) {
  const { icon: Icon, onClick = () => {} } = props;
  return <Icon onClick={onClick} style={{ display: 'inline' }} />;
}
