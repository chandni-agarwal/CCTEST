import { TFunction } from "i18next";

export const getInitAdaptiveSurveyCard = (t: TFunction) => {
    const titleTextAsString = t("TitleText");
    return (
     {
       "type": "AdaptiveCard",
       "body": [
         {
            "type": "TextBlock",
            "text": "Hello Team, please fill this out.Thank you!",
            "wrap": true
         },
        {
            "type": "TextBlock",
            "text": "",
            "wrap": true
        },
        {
            "type": "TextBlock",
            "text": "",
            "wrap": true
        },
        {
            "type": "TextBlock",
            "text": "",
            "wrap": true
        },
        {
            "type": "TextBlock",
            "text": "",
            "wrap": true
        }
    ],
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "version": "1.0",
  }
 );
}

export const getCardName = (card: any) => {
    return card.body[0].text;
}

export const setCardName = (card: any, name: string) => {
    card.body[0].text = name;
}
export const getCardDepartment = (card: any) => {
    return card.body[1].text;
}

export const setCardDepartment = (card: any, department: string) => {
    card.body[1].text = department;
}
export const getCardChoice = (card: any) => {
    return card.body[2].text;
}

export const setCardChoice = (card: any, choice: string) => {
    card.body[2].text = choice;
}
export const getCardReason = (card: any) => {
    return card.body[3].text;
}

export const setCardReason = (card: any, reason: string) => {
    card.body[3].text = reason;
}

export const setCardSurveyBtn = (card: any) => {
        card.actions = [
            {
                "type": "Action.Submit",
                "title": "Submit"
            }
        ]
}