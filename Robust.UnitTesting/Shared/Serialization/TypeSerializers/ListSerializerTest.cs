﻿using System.Collections.Generic;
using NUnit.Framework;
using Robust.Shared.Serialization.Manager;
using Robust.Shared.Serialization.Markdown.Sequence;
using Robust.Shared.Serialization.Markdown.Value;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Generic;

// ReSharper disable AccessToStaticMemberViaDerivedType

namespace Robust.UnitTesting.Shared.Serialization.TypeSerializers
{
    [TestFixture]
    [TestOf(typeof(ListSerializers<>))]
    public sealed class ListSerializerTest : SerializationTest
    {
        [Test]
        public void SerializationTest()
        {
            var list = new List<string> {"A", "E"};
            var node = Serialization.WriteValueAs<SequenceDataNode>(list);

            Assert.That(node.Cast<ValueDataNode>(0).Value, Is.EqualTo("A"));
            Assert.That(node.Cast<ValueDataNode>(1).Value, Is.EqualTo("E"));
        }

        [Test]
        public void DeserializationTest()
        {
            var list = new List<string> {"A", "E"};
            var node = new SequenceDataNode("A", "E");
            var deserializedList = Serialization.Read<List<string>>(node);

            Assert.That(deserializedList, Is.EqualTo(list));
        }

        [Test]
        public void CustomReadTest()
        {
            var node = new SequenceDataNode("A", "E");

            var result = Serialization.Read<List<string>, SequenceDataNode, ListSerializers<string>>(node);
            var list = (List<string>?) result;

            Assert.NotNull(list);
            Assert.IsNotEmpty(list!);
            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list, Does.Contain("A"));
            Assert.That(list, Does.Contain("E"));
        }

        [Test]
        public void CustomWriteTest()
        {
            var list = new List<string> {"A", "E"};

            var node = (SequenceDataNode) Serialization.WriteValue<List<string>, ListSerializers<string>>(list);

            Assert.That(node.Sequence.Count, Is.EqualTo(2));
            Assert.That(node.Cast<ValueDataNode>(0).Value, Is.EqualTo("A"));
            Assert.That(node.Cast<ValueDataNode>(1).Value, Is.EqualTo("E"));
        }

        [Test]
        public void CustomCopyTest()
        {
            var source = new List<string> {"A", "E"};
            var target = new List<string>();

            Assert.IsNotEmpty(source);
            Assert.IsEmpty(target);

            Serialization.CopyTo<List<string>, ListSerializers<string>>(source, ref target);

            Assert.NotNull(source);

            Assert.IsNotEmpty(source);
            Assert.IsNotEmpty(target!);

            Assert.That(source, Is.EqualTo(target));

            Assert.That(source.Count, Is.EqualTo(2));
            Assert.That(target!.Count, Is.EqualTo(2));

            Assert.That(source, Does.Contain("A"));
            Assert.That(source, Does.Contain("E"));

            Assert.That(target, Does.Contain("A"));
            Assert.That(target, Does.Contain("E"));
        }
    }
}
