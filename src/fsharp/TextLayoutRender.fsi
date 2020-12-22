// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

/// DSL to create structured layout objects with optional breaks and render them
namespace FSharp.Compiler.TextLayout

open System.Text
open System.IO
open FSharp.Compiler
open FSharp.Compiler.TextLayout

/// An enhancement to TaggedText in the TaggedText layouts generated by FSharp.Compiler.Service
type public NavigableTaggedText =
    internal new : TaggedText * Range -> NavigableTaggedText
    member Range: Range
    inherit TaggedText

/// Render a Layout yielding an 'a using a 'b (hidden state) type 
type internal LayoutRenderer<'a,'b> =
    abstract Start    : unit -> 'b
    abstract AddText  : 'b -> TaggedText -> 'b
    abstract AddBreak : 'b -> int -> 'b
    abstract AddTag   : 'b -> string * (string * string) list * bool -> 'b
    abstract Finish   : 'b -> 'a

type internal NoState = NoState
type internal NoResult = NoResult

module public LayoutRender = 

    /// Run a render on a Layout
    val public emitL  : (TaggedText -> unit) -> Layout -> unit

    val internal mkNav : Range -> TaggedText -> TaggedText

    val internal showL: Layout -> string

    val internal outL: TextWriter -> Layout -> unit

    val internal bufferL: StringBuilder -> Layout -> unit

    /// Run a render on a Layout       
    val internal renderL  : LayoutRenderer<'b,'a> -> Layout -> 'b

    /// Render layout to string 
    val internal stringR  : LayoutRenderer<string,string list>

    /// Render layout to channel
    val internal channelR : TextWriter -> LayoutRenderer<NoResult,NoState>

    /// Render layout to StringBuilder
    val internal bufferR  : StringBuilder -> LayoutRenderer<NoResult,NoState>

    /// Render layout to collector of TaggedText
    val internal taggedTextListR  : collector: (TaggedText -> unit) -> LayoutRenderer<NoResult, NoState>

module internal SepL =
    val dot: Layout
    val star: Layout
    val colon: Layout
    val questionMark: Layout
    val leftParen: Layout
    val comma: Layout
    val space: Layout
    val leftBracket: Layout
    val leftAngle: Layout
    val lineBreak: Layout
    val rightParen: Layout

module internal WordL =
    val arrow: Layout
    val star: Layout
    val colon: Layout
    val equals: Layout
    val keywordNew: Layout
    val structUnit: Layout
    val keywordStatic: Layout
    val keywordMember: Layout
    val keywordVal: Layout
    val keywordEvent: Layout
    val keywordWith: Layout
    val keywordSet: Layout
    val keywordGet: Layout
    val keywordTrue: Layout
    val keywordFalse: Layout
    val bar: Layout
    val keywordStruct: Layout
    val keywordInherit: Layout
    val keywordEnd: Layout
    val keywordNested: Layout
    val keywordType: Layout
    val keywordDelegate: Layout
    val keywordOf: Layout
    val keywordInternal: Layout
    val keywordPrivate: Layout
    val keywordAbstract: Layout
    val keywordOverride: Layout
    val keywordEnum: Layout

module internal LeftL =
    val leftParen: Layout
    val questionMark: Layout
    val colon: Layout
    val leftBracketAngle: Layout
    val leftBracketBar: Layout
    val keywordTypeof: Layout
    val keywordTypedefof: Layout

module internal RightL =
    val comma: Layout
    val rightParen: Layout
    val colon: Layout
    val rightBracket: Layout
    val rightAngle: Layout
    val rightBracketAngle: Layout
    val rightBracketBar: Layout

