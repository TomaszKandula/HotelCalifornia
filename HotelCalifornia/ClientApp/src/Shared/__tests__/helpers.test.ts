import * as helpers from "../helpers";
import { IErrorDto } from "../../Models";
import { VALIDATION_ERRORS } from "../../Shared/constants";

describe("Verify helper methods.", () => 
{
    test("Should convert object props to an array of fields values.", () => 
    {
        const testObject = 
        {
            FieldA: "ValueA",
            FieldB: "ValueB"
        };
    
        const expectation: string[] = ["ValueA", "ValueB"];
    
        expect(
            helpers.ConvertPropsToFields(testObject).sort()
        ).toEqual(
            expectation.sort()
        );   
    });

    test("Should render HTML line with given HTML tag or return whitespace for undefined input.", () => 
    {
        const testTag: string = "li";
        const testItem1: string = "This is test item";
        const testItem2 = undefined;
    
        const expectation1: string = "<li>This is test item</li>";
        const expectation2 = " ";
    
        expect(helpers.HtmlRenderLine(testTag, testItem1)).toBe(expectation1);
        expect(helpers.HtmlRenderLine(testTag, testItem2)).toBe(expectation2);    
    });

    test("Should render multiple lines of HTML code with given tag.", () => 
    {    
        const testArray: string[] = ["ValueA", "ValueB"];    
        const testTag: string = "il";
    
        const expectation: string = "<il>ValueA</il><il>ValueB</il>";
    
        expect(helpers.HtmlRenderLines(testArray, testTag)).toBe(expectation);
    });

    test("Should return translated error message", () => 
    {
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "ErrorCode": "USERNAME_ALREADY_EXISTS",
                    "ErrorMessage": "This user name already exists",
                    "ValidationErrors": null
                }
            }
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const expectation: string = "This user name already exists";

        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });

    test("Should return translated error message with validation errors", () => 
    {
        const jsonObject: string = `
        {
            "response": 
            {
                "data":
                {
                    "ErrorCode": "CANNOT_ADD_DATA",
                    "ErrorMessage": "Cannot add invalid data",
                    "ValidationErrors": 
                    [
                        {
                            "PropertyName": "Id",
                            "ErrorCode": "INVALID_GUID",
                            "ErrorMessage": "Must be GUID"
                        },
                        {
                            "PropertyName": "UserAge",
                            "ErrorCode": "INVALID_NUMBER",
                            "ErrorMessage": "Cannot be negative number"
                        }
                    ]
                }
            }            
        }`

        const textObject: IErrorDto = JSON.parse(jsonObject) as IErrorDto;
        const expectation: string = "Cannot add invalid data. " + VALIDATION_ERRORS;

        expect(helpers.GetErrorMessage(textObject)).toBe(expectation);
    });
});