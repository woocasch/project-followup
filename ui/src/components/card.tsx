import styled from '@emotion/styled';
import { theme } from '@root/theme';
import type { ReactNode } from 'react';

export enum Size {
    Small = "small",
    Medium = "medium",
    Large = "large"
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

const CardContainer = (hasBorder: boolean, size: Size) => {
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

    return styled.div(`
    overflow: hidden;
    border-radius: ${radiusValue};
    ${hasBorder && `border: 1px solid black;`}
`);
}

const CardHeaderStyled = styled.div(`
    background-color: ${theme.colors.primary};
    padding: ${theme.spaces.xsmall};
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
    return <CardHeaderStyled>{children}</CardHeaderStyled>;
}

function CardContent({ children }: CardSectionProps) {
    return <CardContentStyled>{children}</CardContentStyled>;
}

function CardFooter({ children }: CardSectionProps) {
    return <CardFooterStyled>{children}</CardFooterStyled>;
}

function CardRoot(props: CardProps) {
    const { children, hasBorder = false, size = Size.Medium } = props;
    const Container = CardContainer(hasBorder, size);
    return <Container>{children}</Container>;
}

export const Card = Object.assign(CardRoot, {
    Header: CardHeader,
    Content: CardContent,
    Footer: CardFooter,
});