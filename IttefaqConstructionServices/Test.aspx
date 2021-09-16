﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="IttefaqConstructionServices.Test" %>

<!DOCTYPE HTML>
<!-- saved from url=(0037)https://metroui.org.ua/validator.html -->
<!DOCTYPE html PUBLIC "" "">
<html>
<head lang="en">
    <meta content="IE=11.0000"
        http-equiv="X-UA-Compatible">

    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="description" content="Metro, a sleek, intuitive, and powerful framework for faster and easier web development for Windows Metro Style.">
    <meta name="keywords" content="HTML, CSS, JS, JavaScript, framework, metro, front-end, frontend, web development">
    <meta name="author" content="Sergey Pimenov and Metro UI CSS contributors">
    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon">
    <title>Form Validator :: Metro UI CSS - The front-end framework for developing projects on the web in Windows Metro Style</title>
    <link href="Metro-UI-CSS-master/build/css/metro.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-icons.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-responsive.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/build/css/metro-schemes.css" rel="stylesheet" />
    <link href="Metro-UI-CSS-master/docs/css/docs.css" rel="stylesheet" />

    <script src="Metro-UI-CSS-master/docs/js/metro.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/docs.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/prettify/run_prettify.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/ga.js"></script>
    <script src="Metro-UI-CSS-master/docs/js/jquery-2.1.3.min.js"></script>
    <script src="Metro-UI-CSS-master/fUWHN94OD.txt" async=""></script>

    <script>
        function no_submit() {
            return false;
        }

        function notifyOnErrorInput(input) {
            var message = input.data('validateHint');
            $.Notify({
                caption: 'Error',
                content: message,
                type: 'alert'
            });
        }
    </script>

    <meta name="GENERATOR" content="MSHTML 11.00.9600.18739">
</head>
<body>
    <div class="container page-content">
        <h1><a class="nav-button transform"
            href="https://metroui.org.ua/index.html"><span></span></a>&nbsp;Form validator</h1>
        <!-- Google adsense block -->
        <div style="margin: 10px 0px;">
            <ins class="adsbygoogle" style="display: block;"
                data-ad-format="auto" data-ad-slot="8347181909" data-ad-client="ca-pub-1632668592742327"></ins>
        </div>
        <script>
            (adsbygoogle = window.adsbygoogle || []).push({});
</script>
        <!-- End of gad block -->
        <div class="margin20 no-margin-left no-margin-right sub-header text-light">
            This widget makes simple clientside form validation easy, whilst still 
offering plenty of customization options. The widget comes bundled 
with a useful set of validation methods, including URL, email, hex color, min 
and max values, length validation.        
        </div>
        <div class="example" data-text="example">
            <div class="grid">
                <div class="row cells3">
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit">
                            <label class="block">
                                Min length control (state)</label>
                            <div
                                class="input-control text">
                                <input type="text" data-show-error-hint="false" data-validate-arg="6" data-validate-func="minlength">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div>
                                <button class="button success">Send</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-easing="easeOutBounce"
                            data-hint-mode="hint">
                            <label class="block">Min length control (hint)</label>
                            <div
                                class="input-control text">
                                <input type="text" data-validate-arg="6" data-validate-func="minlength" data-validate-hint-position="top" data-validate-hint="Min length 6 sym !">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div>
                                <button class="button success">Send</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit"
                            data-show-error-hint="false" data-on-error-input="notifyOnErrorInput">
                            <label
                                class="block">
                                Combine with notify system</label>
                            <div
                                class="input-control text">
                                <input type="text" data-validate-arg="6" data-validate-func="required" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div>
                                <button class="button success">Send</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="example" data-text="code">
            <h5>Simple</h5>
            <pre class="prettyprint linenums"><CODE>
                &lt;form data-role="validator"&gt;
                    &lt;label class="block"&gt;Min length control&lt;/label&gt;
                    &lt;div class="input-control text"&gt;
                        &lt;input
                            data-validate-func="minlength"
                            data-validate-arg="6"
                            data-validate-hint="This field must contains min 6 symbols!"
                            type="text"&gt;
                        &lt;span class="input-state-error mif-warning"&gt;&lt;/span&gt;
                        &lt;span class="input-state-success mif-checkmark"&gt;&lt;/span&gt;
                    &lt;/div&gt;
                    &lt;div&gt;
                        &lt;button class="button success"&gt;Send&lt;/button&gt;
                    &lt;/div&gt;
                &lt;/form&gt;
            </CODE></pre>
            <h5>Combine with notify system</h5>
            <pre class="prettyprint linenums"><CODE>
                &lt;form
                    data-role="validator"
                    data-on-error-input="notifyOnErrorInput"
                    data-show-error-hint="false"&gt;
                    &lt;label class="block"&gt;Combine with notify system&lt;/label&gt;
                    &lt;div class="input-control text"&gt;
                        &lt;input type="text"
                            data-validate-func="required"
                            data-validate-hine="This field can not be empty!"&gt;
                        &lt;span class="input-state-error mif-warning"&gt;&lt;/span&gt;
                        &lt;span class="input-state-success mif-checkmark"&gt;&lt;/span&gt;
                    &lt;/div&gt;
                    &lt;div&gt;
                        &lt;button class="button success"&gt;Send&lt;/button&gt;
                    &lt;/div&gt;
                &lt;/form&gt;

                &lt;script&gt;
                    function notifyOnErrorInput(input){
                        var message = input.data('validateHint');
                        $.Notify({
                            caption: 'Error',
                            content: message,
                            type: 'alert'
                        });
                    }
                &lt;/script&gt;
            </CODE></pre>
        </div>
        <h3>Validating functions</h3>
        <table class="table border bordered">
            <thead>
                <tr>
                    <th>Func name</th>
                    <th>Description</th>
                    <th>Params</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><strong>required</strong></td>
                    <td>Field can not be empty</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>minlength</strong></td>
                    <td>Control min length of value</td>
                    <td>integer</td>
                </tr>
                <tr>
                    <td><strong>maxlength</strong></td>
                    <td>Control max length of value</td>
                    <td>integer</td>
                </tr>
                <tr>
                    <td><strong>min</strong></td>
                    <td>Control min value for numeric</td>
                    <td>number</td>
                </tr>
                <tr>
                    <td><strong>max</strong></td>
                    <td>Control max value for numeric</td>
                    <td>number</td>
                </tr>
                <tr>
                    <td><strong>email</strong></td>
                    <td>Control valid email address</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>url</strong></td>
                    <td>Control valid url address</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>date</strong></td>
                    <td>Control valid date string</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>number</strong></td>
                    <td>Control valid number value</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>digits</strong></td>
                    <td>Control only digits in value</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>hexcolor</strong></td>
                    <td>Control valid hex color value</td>
                    <td>no</td>
                </tr>
                <tr>
                    <td><strong>pattern</strong></td>
                    <td>Custom regexp pattern for control value</td>
                    <td>string pattern</td>
                </tr>
            </tbody>
        </table>
        <h3>Validator options</h3>
        <table class="table bordered border">
            <thead>
                <tr>
                    <td>Parameter</td>
                    <td>Data-*</td>
                    <td>Type</td>
                    <td>Default value</td>
                    <td>Description</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>showErrorState</td>
                    <td style="white-space: nowrap;">data-show-error-state</td>
                    <td>boolean</td>
                    <td>true</td>
                    <td>If true input change color state</td>
                </tr>
                <tr>
                    <td>showErrorHint</td>
                    <td style="white-space: nowrap;">data-show-error-hint</td>
                    <td>boolean</td>
                    <td>true</td>
                    <td>If true over input showing hint</td>
                </tr>
                <tr>
                    <td>showRequiredState</td>
                    <td style="white-space: nowrap;">data-show-required-state</td>
                    <td>boolean</td>
                    <td>true</td>
                    <td>If true input with validate func showing with required color 
  state</td>
                </tr>
                <tr>
                    <td>showSuccessState</td>
                    <td style="white-space: nowrap;">data-show-success-state</td>
                    <td>boolean</td>
                    <td>true</td>
                    <td>If true valid input with func showing with success color state</td>
                </tr>
                <tr>
                    <td>hintSize</td>
                    <td style="white-space: nowrap;">data-hint-size</td>
                    <td>int</td>
                    <td>200</td>
                    <td>Min width of hint size</td>
                </tr>
                <tr>
                    <td>hintBackground</td>
                    <td style="white-space: nowrap;">data-hint-background</td>
                    <td>string</td>
                    <td>#FFFCC0</td>
                    <td>Hint background color, can be hex color or class name</td>
                </tr>
                <tr>
                    <td>hintColor</td>
                    <td style="white-space: nowrap;">data-hint-color</td>
                    <td>string</td>
                    <td>#000000</td>
                    <td>Hint text color, can be hex color or class name</td>
                </tr>
                <tr>
                    <td>hideError</td>
                    <td style="white-space: nowrap;">data-hide-error</td>
                    <td>int</td>
                    <td>2000</td>
                    <td>Time interval before error state disabled (msec)</td>
                </tr>
                <tr>
                    <td>hideHint</td>
                    <td style="white-space: nowrap;">data-hide-hint</td>
                    <td>int</td>
                    <td>5000</td>
                    <td>Time interval before hint hided (msec)</td>
                </tr>
                <tr>
                    <td>hintEasing</td>
                    <td style="white-space: nowrap;">data-hint-easing</td>
                    <td>string</td>
                    <td>easeInQuad</td>
                    <td>Easing func for animate showing hint</td>
                </tr>
                <tr>
                    <td>hintEasingTime</td>
                    <td style="white-space: nowrap;">data-hint-easing-time</td>
                    <td>int</td>
                    <td>400</td>
                    <td>Easing animate time</td>
                </tr>
                <tr>
                    <td>hintMode</td>
                    <td style="white-space: nowrap;">data-hint-mode</td>
                    <td>string</td>
                    <td>hint</td>
                    <td>Hint type, can be: hint or line</td>
                </tr>
                <tr>
                    <td>hintPosition</td>
                    <td style="white-space: nowrap;">data-hint-position</td>
                    <td>string</td>
                    <td>right</td>
                    <td>Hint position, can be: right, left, top or bottom</td>
                </tr>
            </tbody>
        </table>
        <h3>Hint position <small>hint mode:  <span class="tag">hint</span></small> </h3>
        <div class="example" data-text="example">
            <div class="grid">
                <div class="row cells4">
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-easing="easeOutBounce"
                            data-hint-mode="hint">
                            <label>Top</label>
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="top" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-easing="easeOutBounce"
                            data-hint-mode="hint">
                            <label>Left</label>
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="left" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-easing="easeOutBounce"
                            data-hint-mode="hint">
                            <label>Right</label>
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="right" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-easing="easeOutBounce"
                            data-hint-mode="hint">
                            <label>Bottom</label>
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="bottom" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <h3>Hint position <small>hint mode: <span class="tag">line</span></small></h3>
        <div class="example" data-text="example">
            <div class="grid">
                <div class="row cells4">
                    <div class="cell">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-mode="line">
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="top" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell colspan3">
                        <form data-role="validator" data-on-before-submit="no_submit" data-hint-mode="line">
                            <div class="input-control text full-size">
                                <input type="text" data-validate-func="required" data-validate-hint-position="top" data-validate-hint="This field can not be empty!">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div class="align-center">
                                <button class="button info">Submit</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <h3>Func examples</h3>
        <form data-role="validator" data-hint-mode="line" data-hide-error="5000"
            data-hint-color="fg-white" data-hint-background="bg-red"
            data-show-required-state="false">
            <div class="grid">
                <div class="row">
                    <div class="cell">
                        <div class="input-control text full-size">
                            <input type="text" placeholder="default, no validate" value="">
                            <button class="button helper-button clear">
                                <span
                                    class="mif-cross"></span>
                            </button>
                        </div>
                        <div class="input-control text"
                            data-role="input">
                            <input type="text" placeholder="not empty" value="" data-validate-func="required" data-validate-hint="This field can not be empty">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="min length 3" value="" data-validate-arg="3" data-validate-func="minlength" data-validate-hint="Min length 3 symbols">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="max length 6" value="" data-validate-arg="6" data-validate-func="maxlength" data-validate-hint="Max length 6 symbols">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="min value 10" value="" data-validate-arg="10" data-validate-func="min" data-validate-hint="Value >= 10">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="max value 10" value="" data-validate-arg="10" data-validate-func="max" data-validate-hint="Value <= 10">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="email" value="" data-validate-func="email" data-validate-hint="Not valid email address">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="url" value="" data-validate-func="url" data-validate-hint="Value must be valid url">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="date" value="" data-validate-func="date" data-validate-hint="Value must be valid date string">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="number" value="" data-validate-func="number" data-validate-hint="Value must be a numeric">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="digits" value="" data-validate-func="digits" data-validate-hint="Value must contains only digits">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="hex color" value="" data-validate-func="hexcolor" data-validate-hint="Value must be a valid hex color">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                        <div class="input-control text "
                            data-role="input">
                            <input type="text" placeholder="custom pattern: ^\d+$" value="" data-validate-arg="^\d+$" data-validate-func="pattern" data-validate-hint="Value must be eq to pattern">
                            <span class="input-state-error mif-warning"></span>
                            <span
                                class="input-state-success mif-checkmark"></span>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="thin">

            <div class="form-actions padding10 no-padding-left">
                <button class="button" type="submit">Submit</button>
            </div>
        </form>
        <h3>Multi func</h3>
        <div class="example" data-text="code">
            <pre class="prettyprint linenums no-scroll"><CODE>
                &lt;input type="text" data-validate-func="required, number"&gt;

                &lt;input type="text"
                    data-validate-func="required, number, minlength"
                    data-validate-arg=",,6"&gt;
            </CODE></pre>
        </div>
        <form action="javascript:void(0)" data-role="validator" data-on-submit="submit">
            <div class="input-control text block" style="width: 300px;"
                data-role="input">
                <input type="text" placeholder="not empty, number" value="" data-validate-func="required, number" data-validate-hint="This field can not be empty and can be number">
                <span class="input-state-error mif-warning"></span>
                <span class="input-state-success mif-checkmark"></span>
            </div>
            <div class="input-control text block" style="width: 300px;"
                data-role="input">
                <input type="text" placeholder="not empty, number, min length 6" value="" data-validate-arg=",,6" data-validate-func="required, number, minlength" data-validate-hint="This field can not be empty and can be number and min length 6 digits">
                <span class="input-state-error mif-warning"></span>
                <span class="input-state-success mif-checkmark"></span>
            </div>
            <hr class="thin">

            <div class="form-actions padding10 no-padding-left">
                <button class="button" type="submit">Submit</button>
            </div>
        </form>
        <h3>onSubmit</h3>
        <form action="javascript:void(0)" data-role="validator" data-on-submit="submit">
            <div class="input-control text"
                data-role="input">
                <input type="text" placeholder="not empty" value="" data-validate-func="required" data-validate-hint="This field can not be empty">
                <span class="input-state-error mif-warning"></span>
                <span class="input-state-success mif-checkmark"></span>
            </div>
            <label class="block">Категория</label>
            <div class="input-control select full-size" data-role="input">
                <select name="employee_category"
                    data-validate-func="required">
                    <option value="" selected="">Выберите 
  категорию</option>
                    <option value="1">категория 1</option>
                    <option value="2">категория 2</option>
                    <option value="3">категория 3</option>
                    <option
                        value="4">категория 4</option>
                </select>
            </div>
            <div class="form-actions padding10 no-padding-left">
                <button class="button" type="submit">Submit</button>
            </div>
        </form>
        <script>
            function submit(form) {
                console.log(form);
                return true;
            }
        </script>

        <h3>onSubmit 2</h3>
        <div class="example" data-text="example">
            <div class="grid">
                <div class="row cells2">
                    <div class="cell">
                        <form data-role="validator" data-on-submit="return false">
                            <label
                                class="block">
                                data-on-submit: return false</label>
                            <div
                                class="input-control text">
                                <input type="text" data-show-error-hint="false" data-validate-func="required">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div>
                                <button class="button success">Send</button>
                            </div>
                        </form>
                    </div>
                    <div class="cell">
                        <form data-role="validator" data-on-submit="return true">
                            <label
                                class="block">
                                data-on-submit: return true</label>
                            <div
                                class="input-control text">
                                <input type="text" data-show-error-hint="false" data-validate-func="required">
                                <span
                                    class="input-state-error mif-warning"></span>
                                <span class="input-state-success mif-checkmark"></span>
                            </div>
                            <div>
                                <button class="button success">Send</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
