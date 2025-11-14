import styled from '@emotion/styled';
import { theme } from '@root/theme';
import type { ReactNode } from 'react';

export enum Size {
  Small = 'small',
  Medium = 'medium',
  Large = 'large',
}

interface DisplaySettings {
  hasBorder?: boolean;
  size?: Size;
}

interface CardProps extends DisplaySettings {
  children: ReactNode;
}

interface CardSectionProps {
  children: ReactNode;
}

function CalculateBorderRadius(size: Size) {
  let radiusValue = theme.borderRadius.medium;
  switch (size) {
    case Size.Small:
      radiusValue = theme.borderRadius.xsmall;
      break;
    case Size.Medium:
      radiusValue = theme.borderRadius.medium;
      break;
    case Size.Large:
      radiusValue = theme.borderRadius.xlarge;
      break;
  }
  return radiusValue;
}

interface CardContainerProps {
  size: Size;
  hasBorder: boolean;
}

const CardContainer = styled('div')<CardContainerProps>`
        overflow: hidden;
        border-radius: ${(props: CardContainerProps) => CalculateBorderRadius(props.size)};

        & .header, & .content, & .footer {
            padding-top: 0.25em;
            padding-bottom: 0.25em;
            padding-right: calc(${(props: CardContainerProps) => CalculateBorderRadius(props.size)} + 0.25em);
            padding-left: calc(${(props: CardContainerProps) => CalculateBorderRadius(props.size)} + 0.25em);
        }
    `;

const CardHeaderStyled = styled.div(`
    background-color: ${theme.colors.primary};
`);

const CardContentStyled = styled.div(`
    background-color: ${theme.colors.secondary};
    padding: ${theme.spaces.xsmall};
`);

const CardFooterStyled = styled.div(`
    background-color: ${theme.colors.primary};
    padding: ${theme.spaces.xsmall};
`);

function CardHeader({ children }: CardSectionProps) {
  return <CardHeaderStyled className="header">{children}</CardHeaderStyled>;
}

function CardContent({ children }: CardSectionProps) {
  return <CardContentStyled className="content">{children}</CardContentStyled>;
}

function CardFooter({ children }: CardSectionProps) {
  return <CardFooterStyled className="footer">{children}</CardFooterStyled>;
}

function CardRoot(props: CardProps) {
  const { children, hasBorder = false, size = Size.Medium } = props;
  return (
    <CardContainer hasBorder={hasBorder} size={size}>
      {children}
    </CardContainer>
  );
}

export const Card = Object.assign(CardRoot, {
  Header: CardHeader,
  Content: CardContent,
  Footer: CardFooter,
});
