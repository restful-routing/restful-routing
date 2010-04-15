require "uv"

module Haml
  module Filters
    module Highlight_Cs
      include Base

      def render(text)
        text = text.strip
        result = Uv.parse( text, "xhtml", "cs", false, "twilight")
        Haml::Helpers.preserve result
      end
    end
    
    module Highlight_Plain
      include Base

      def render(text)
        result = "<pre class='twilight'>#{Haml::Helpers.preserve(text)}</pre>"
      end
    end
  end
end