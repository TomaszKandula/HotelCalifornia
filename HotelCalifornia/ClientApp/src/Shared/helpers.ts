import Validate from "validate.js";
import { IErrorDto } from "../Models";
import { UNEXPECTED_ERROR, VALIDATION_ERRORS } from "./constants";

const ConvertPropsToFields = (InputObject: any) =>
{
    let resultArray: any[] = [];

    for (let Property in InputObject) 
        resultArray = resultArray.concat(InputObject[Property]); 
    
    return resultArray;
}

const HtmlRenderLine = (Tag: string, Text: string | undefined) => 
{
    return Validate.isDefined(Text) ? `<${Tag}>${Text}</${Tag}>` : " ";
}

const HtmlRenderLines = (InputArray: any[], Tag: string) =>
{
    let result: string = "";
    let htmlLine: string = "";

    for (let Item of InputArray)
    {
        htmlLine = HtmlRenderLine(Tag, Item); 
        if (!Validate.isEmpty(htmlLine)) 
            result = result.concat(htmlLine);
    }

    return result;
}

const GetErrorMessage = (errorObject: any): string =>
{
    console.error(errorObject);
    let result: string = UNEXPECTED_ERROR;

    if (errorObject?.response?.data)
    {
        const parsedJson: IErrorDto = errorObject.response.data as IErrorDto;
        result = Validate.isEmpty(parsedJson.ErrorMessage) 
            ? UNEXPECTED_ERROR 
            : parsedJson.ErrorMessage;

        if (parsedJson.ValidationErrors !== null) 
        {
            result = result + ". " + VALIDATION_ERRORS;
        }
    }

    return result;
}

export 
{
    ConvertPropsToFields,
    HtmlRenderLine,
    HtmlRenderLines,
    GetErrorMessage
}